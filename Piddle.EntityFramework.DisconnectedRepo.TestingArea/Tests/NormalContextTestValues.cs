using Microsoft.VisualStudio.TestTools.UnitTesting;
using Piddle.EntityFramework.DisconnectedRepo.TestingArea.EntityBase;
using Piddle.EntityFramework.DisconnectedRepo.TestingArea.NormalContext;
using System;
using System.Linq;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.Tests
{
    public class NormalContextTestValues
    {
        public int MealCount { get; set; }
        public int CourseCount { get; set; }
        public NormalBillEntity Bill { get; set; }
        public NormalFoodCourseEntity Cereal1 { get; set; }
        public NormalFoodCourseEntity Cereal2 { get; set; }
        public NormalFoodCourseEntity FryUp { get; set; }
        public NormalMealEntity CerealAndFryUpBreakfast { get; set; }
        public NormalMealEntity CerealOnlyBreakfast { get; set; }

        public NormalContextTestValues()
        {
            Cereal1 = new NormalFoodCourseEntity()
            {
                Name = "Generic cereal with milk",
                Cost = 2.20M,
                Type = FoodCourseType.Starter,
                LastUpdated = DateTime.Now
            };

            Cereal2 = new NormalFoodCourseEntity()
            {
                Name = "Generic cereal with soya milk",
                Cost = 2.20M,
                Type = FoodCourseType.Starter,
                LastUpdated = DateTime.Now
            };

            FryUp = new NormalFoodCourseEntity()
            {
                Name = "Bacon, eggs, sausage, etc",
                Cost = 6.00M,
                Type = FoodCourseType.Main,
                LastUpdated = DateTime.Now
            };

            CerealAndFryUpBreakfast = new NormalMealEntity()
            {
                Name = "Cereal and fry-up breakfast",
                LastUpdated = DateTime.Now
            };

            CerealAndFryUpBreakfast.Courses.Add(Cereal1);
            CerealAndFryUpBreakfast.Courses.Add(FryUp);

            CerealOnlyBreakfast = new NormalMealEntity()
            {
                Name = "Cereal only breakfast",
                LastUpdated = DateTime.Now
            };

            CerealOnlyBreakfast.Courses.Add(Cereal2);

            Bill = new NormalBillEntity()
            {
                LastUpdated = DateTime.Now
            };

            Bill.Meals.Add(CerealAndFryUpBreakfast);
            Bill.Meals.Add(CerealOnlyBreakfast);

            CourseCount = Bill.Meals.SelectMany(x => x.Courses).Count();
            MealCount = Bill.Meals.Count();
        }

        public void AssertCreated(int rowsAffected)
        {
            // All 6 entities should be inserted

            Assert.AreEqual(6, rowsAffected, "Rows affected mismatch");
            Assert.AreNotEqual(default(long), Bill.BillId, "Bill not created");
            Assert.AreNotEqual(default(long), CerealAndFryUpBreakfast.MealId, "CerealAndFryUpBreakfast not created");
            Assert.AreNotEqual(default(long), CerealOnlyBreakfast.MealId, "CerealOnlyBreakfast not created");
            Assert.AreNotEqual(default(long), Cereal1.FoodCourseId, "Cereal1 not created");
            Assert.AreNotEqual(default(long), Cereal2.FoodCourseId, "Cereal2 not created");
            Assert.AreNotEqual(default(long), FryUp.FoodCourseId, "FryUp not created");

            // Bill should be wired up to CerealAndFryUpBreakfast, CerealOnlyBreakfast, Cereal1, Cereal2 and FryUp

            Assert.AreEqual(2, Bill.Meals.Count, "Bill should have 2 meals");
            Assert.AreEqual(3, Bill.Courses.Count, "Bill should have 3 courses");

            Assert.AreEqual(Bill, CerealAndFryUpBreakfast.Bill, "Bill != CerealAndFryUpBreakfast.Bill");
            Assert.AreEqual(Bill.BillId, CerealAndFryUpBreakfast.BillId, "Bill.BillId != CerealAndFryUpBreakfast.BillId");
            Assert.IsTrue(Bill.Meals.Contains(CerealAndFryUpBreakfast), "Bill should have CerealAndFryUpBreakfast meal");

            Assert.AreEqual(Bill, CerealOnlyBreakfast.Bill, "Bill != CerealOnlyBreakfast.Bill");
            Assert.AreEqual(Bill.BillId, CerealOnlyBreakfast.BillId, "Bill.BillId != CerealOnlyBreakfast.BillId");
            Assert.IsTrue(Bill.Meals.Contains(CerealOnlyBreakfast), "Bill should have CerealOnlyBreakfast meal");

            Assert.AreEqual(Bill, Cereal1.Bill, "Bill != Cereal1.Bill");
            Assert.AreEqual(Bill.BillId, Cereal1.BillId, "Bill.BillId != Cereal1.BillId");
            Assert.IsTrue(Bill.Courses.Contains(Cereal1), "Bill should have Cereal1 course");

            Assert.AreEqual(Bill, Cereal2.Bill, "Bill != Cereal2.Bill");
            Assert.AreEqual(Bill.BillId, Cereal2.BillId, "Bill.BillId != Cereal2.BillId");
            Assert.IsTrue(Bill.Courses.Contains(Cereal2), "Bill should have Cereal2 course");

            Assert.AreEqual(Bill, FryUp.Bill, "Bill != FryUp.Bill");
            Assert.AreEqual(Bill.BillId, FryUp.BillId, "Bill.BillId != FryUp.BillId");
            Assert.IsTrue(Bill.Courses.Contains(FryUp), "Bill should have FryUp course");

            // CerealAndFryUpBreakfast should be wired up to Cereal1 and FryUp

            Assert.AreEqual(2, CerealAndFryUpBreakfast.Courses.Count, "CerealAndFryUpBreakfast should have 2 courses");

            Assert.AreEqual(CerealAndFryUpBreakfast, Cereal1.Meal, "CerealAndFryUpBreakfast != Cereal1.Meal");
            Assert.AreEqual(CerealAndFryUpBreakfast.MealId, Cereal1.MealId, "CerealAndFryUpBreakfast.MealId != Cereal1.MealId");
            Assert.IsTrue(CerealAndFryUpBreakfast.Courses.Contains(Cereal1), "CerealAndFryUpBreakfast should have Cereal1 course");

            Assert.AreEqual(CerealAndFryUpBreakfast, FryUp.Meal, "CerealAndFryUpBreakfast != FryUp.Meal");
            Assert.AreEqual(CerealAndFryUpBreakfast.MealId, FryUp.MealId, "CerealAndFryUpBreakfast.MealId != FryUp.MealId");
            Assert.IsTrue(CerealAndFryUpBreakfast.Courses.Contains(FryUp), "CerealAndFryUpBreakfast should have FryUp course");

            // CerealOnlyBreakfast should be wired up to Cereal2

            Assert.AreEqual(1, CerealOnlyBreakfast.Courses.Count, "CerealOnlyBreakfast should have 1 course");

            Assert.AreEqual(CerealOnlyBreakfast, Cereal2.Meal, "CerealOnlyBreakfast != Cereal2.Meal");
            Assert.AreEqual(CerealOnlyBreakfast.MealId, Cereal2.MealId, "CerealOnlyBreakfast.MealId != Cereal2.MealId");
            Assert.IsTrue(CerealOnlyBreakfast.Courses.Contains(Cereal2), "CerealOnlyBreakfast should have Cereal2 course");
        }
    }
}
