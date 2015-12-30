using Microsoft.VisualStudio.TestTools.UnitTesting;
using Piddle.EntityFramework.DisconnectedRepo.TestingArea.NormalContext;
using System;
using System.Data.Entity;
using System.Linq;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.Tests
{
    [TestClass]
    public class NormalDbContextTests : DbContextTestsBase
    {
        [TestMethod]
        public void TestNormal_Can_AttachFullRootAndTouchTheChildren()
        {
            // Test we can attach Bill and touch all his children

            using (var context = new NormalDbContext())
            {
                // Attach object graph

                context.Set<NormalBillEntity>().Attach(Values.Bill);

                // Touch them

                Values.Bill.LastUpdated = DateTime.Now;
                Values.Cereal1.LastUpdated = DateTime.Now;
                Values.Cereal2.LastUpdated = DateTime.Now;
                Values.CerealAndFryUpBreakfast.LastUpdated = DateTime.Now;
                Values.CerealOnlyBreakfast.LastUpdated = DateTime.Now;
                Values.FryUp.LastUpdated = DateTime.Now;

                // Save

                var rowsAffected = context.SaveChanges();

                // All 6 entities should be modified

                Assert.AreEqual(6, rowsAffected, "Rows affected mismatch");
            }
        }

        [TestMethod]
        public void TestNormal_Cant_ReadNavigationPropertyOutsideContextWithoutDetach()
        {
            // Read Bill with only Courses loaded

            NormalBillEntity bill0;

            using (var context = new NormalDbContext())
            {
                bill0 = context.Bills.Include(x => x.Courses).Single();
            }

            // Meals should be inaccessible

            var threwException = false;

            try
            {
                var meals = bill0.Meals; // Throwing an exception is not helpful
            }
            catch (ObjectDisposedException)
            {
                threwException = true;
            }
            finally
            {
                Assert.IsTrue(threwException, "EF should screw you over because of its proxy object");
            }
        }

        [TestMethod]
        public void TestNormal_Cant_ReadNavigationPropertyOutsideContextAfterDetach()
        {
            // Read Bill with only Courses loaded and detach

            NormalBillEntity bill0;

            using (var context = new NormalDbContext())
            {
                bill0 = context.Bills.Include(x => x.Courses).Single();
                Assert.AreEqual(Values.CourseCount, bill0.Courses.Count, "Bill should have courses loaded");
                context.Entry(bill0).State = EntityState.Detached;
            }

            Assert.IsNotNull(bill0.Meals, "Bill should have a Meals collection"); // This is not helpful, we didn't load them
            Assert.AreEqual(0, bill0.Meals.Count, "Bill should have no Meals"); // This is not helpful either

            Assert.IsNotNull(bill0.Courses, "Bill should have a Courses collection");
            Assert.AreEqual(0, bill0.Courses.Count, "Bill should have no Courses"); // This is not helpful either since we did load the Courses
        }

        [TestMethod]
        public void TestNormal_Cant_AttachOverlappingGraphsFromDifferentContexts()
        {
            // Read Bills with only Courses loaded

            NormalBillEntity bill0;

            using (var context = new NormalDbContext())
            {
                bill0 = context.Bills.Include(x => x.Courses).Single();
            }

            // Read first Meal with Courses loaded

            NormalMealEntity meal0;

            using (var context = new NormalDbContext())
            {
                meal0 = context.Meals.Include(x => x.Courses).First();
            }

            // Now try and attach the graphs

            using (var context = new NormalDbContext())
            {
                context.Set<NormalMealEntity>().Attach(meal0);

                // Overlapping graph should fail

                var threwException = false;

                try
                {
                    context.Set<NormalBillEntity>().Attach(bill0); // Throwing an exception is not helpful
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
