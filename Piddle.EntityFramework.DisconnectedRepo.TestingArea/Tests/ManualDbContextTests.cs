using Microsoft.VisualStudio.TestTools.UnitTesting;
using Piddle.EntityFramework.DisconnectedRepo.TestingArea.ManualContext;
using System;
using System.Data.Entity;
using System.Linq;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.Tests
{
    [TestClass]
    public class ManualDbContextTests : DbContextTestsBase
    {
        [TestMethod]
        public void TestManual_Can_AttachFullRootAndTouchTheChildren()
        {
            // Test we can attach Bill and touch all his children

            var bill = EntityMapper.ToManualBill(Values.Bill);

            using (var context = new ManualDbContext())
            {
                // Attach object graph

                context.Set<ManualBillEntity>().Attach(bill);
                
                // Touch them AND make them dirty

                bill.LastUpdated = DateTime.Now;
                context.Entry(bill).State = EntityState.Modified;
                
                foreach (var foodCourse in bill.Courses)
                {
                    foodCourse.LastUpdated = DateTime.Now;
                    context.Entry(foodCourse).State = EntityState.Modified;
                }

                foreach (var meal in bill.Meals)
                {
                    meal.LastUpdated = DateTime.Now;
                    context.Entry(meal).State = EntityState.Modified;
                }

                // Save

                var rowsAffected = context.SaveChanges();

                // All 6 entities should be modified

                Assert.AreEqual(6, rowsAffected, "Rows affected mismatch");
            }
        }

        [TestMethod]
        public void TestManual_Can_ReadNavigationPropertyOutsideContextWithoutDetach()
        {
            // Read Bill with only Courses loaded and detach

            ManualBillEntity bill0;

            using (var context = new ManualDbContext())
            {
                bill0 = context.Bills.Include(x => x.Courses).Single();
                Assert.AreEqual(Values.CourseCount, bill0.Courses.Count, "Bill should have courses loaded");
            }

            Assert.IsNull(bill0.Meals, "Bill should not have a Meals collection"); // Not loaded, null is a good way of knowing it isn't loaded

            Assert.IsNotNull(bill0.Courses, "Bill should have a Courses collection");
            Assert.AreEqual(Values.CourseCount, bill0.Courses.Count, "Bill should have no Courses"); // Loaded and still there, yay!
        }

        [TestMethod]
        public void TestManual_Cant_ReadNavigationPropertyOutsideContextAfterDetach()
        {
            // Read Bill with only Courses loaded and detach

            ManualBillEntity bill0;

            using (var context = new ManualDbContext())
            {
                bill0 = context.Bills.Include(x => x.Courses).Single();
                Assert.AreEqual(Values.CourseCount, bill0.Courses.Count, "Bill should have courses loaded");
                context.Entry(bill0).State = EntityState.Detached;
            }

            Assert.IsNull(bill0.Meals, "Bill should not have a Meals collection"); // Not loaded, null is a good way of knowing it isn't loaded

            Assert.IsNotNull(bill0.Courses, "Bill should have a Courses collection");
            Assert.AreEqual(0, bill0.Courses.Count, "Bill should have no Courses"); // Not helpful, the detach is a pain
        }

        [TestMethod]
        public void TestManual_Cant_AttachOverlappingGraphsFromDifferentContexts()
        {
            // Read Bills with only Courses loaded

            ManualBillEntity bill0;

            using (var context = new ManualDbContext())
            {
                bill0 = context.Bills.Include(x => x.Courses).Single();
            }

            // Read first Meal with Courses loaded

            ManualMealEntity meal0;

            using (var context = new ManualDbContext())
            {
                meal0 = context.Meals.Include(x => x.Courses).First();
            }

            // Now try and attach the graphs

            using (var context = new ManualDbContext())
            {
                context.Set<ManualMealEntity>().Attach(meal0);

                // Overlapping graph should fail

                var threwException = false;

                try
                {
                    context.Set<ManualBillEntity>().Attach(bill0); // Throwing an exception is not helpful
                }
                catch (InvalidOperationException)
                {
                    threwException = true;
                }
                finally
                {
                    Assert.IsTrue(threwException, "EF should screw you over because it doesn't know what to do with the conflict (type and key match, but different object instance)");
                }
            }
        }
    }
}
