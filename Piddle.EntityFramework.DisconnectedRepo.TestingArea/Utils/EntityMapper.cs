using Piddle.EntityFramework.DisconnectedRepo.TestingArea.ManualContext;
using Piddle.EntityFramework.DisconnectedRepo.TestingArea.NormalContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.Utils
{
    /// <summary>
    /// This class doesn't have any significance in using <see cref="ManualDbContext"/>, it is only used for testing, but a similar
    /// approach could be used for converting between a business model and an entity.
    /// </summary>
    public class EntityMapper
    {
        public ManualBillEntity ToManualBill(NormalBillEntity normalBill)
        {
            if (normalBill == null)
            {
                throw new ArgumentNullException("normalBill");
            }

            var manualBill = new ManualBillEntity()
            {
                BillId = normalBill.BillId,
                LastUpdated = normalBill.LastUpdated,
                Timestamp = normalBill.Timestamp
            };

            if (normalBill.Meals != null)
            {
                manualBill.Meals = normalBill.Meals.Select(x => ToManualMeal(x, manualBill)).ToList();
            }

            if (normalBill.Courses != null)
            {
                manualBill.Courses = new List<ManualFoodCourseEntity>();

                foreach (var course in normalBill.Courses)
                {
                    ManualFoodCourseEntity manualCourse = null;
                    
                    // Try and find from Meals
                    if (manualBill.Meals != null)
                    {
                        manualCourse = FindFoodCourse(manualBill.Meals.SelectMany(x => x.Courses), course.FoodCourseId);
                    }

                    // If not found then convert
                    if (manualCourse == null)
                    {
                        manualCourse = ToManualFoodCourse(course, manualBill, FindMeal(manualBill.Meals, course.MealId));
                    }

                    manualBill.Courses.Add(manualCourse);
                }
            }

            return manualBill;
        }

        public ManualFoodCourseEntity ToManualFoodCourse(NormalFoodCourseEntity normalCourse, ManualBillEntity bill = null, ManualMealEntity meal = null)
        {
            if (normalCourse == null)
            {
                throw new ArgumentNullException("normalCourse");
            }

            if (bill != null && normalCourse.BillId != bill.BillId)
            {
                throw new ArgumentOutOfRangeException("bill");
            }

            if (meal != null && normalCourse.MealId != meal.MealId)
            {
                throw new ArgumentOutOfRangeException("meal");
            }

            var manualCourse = new ManualFoodCourseEntity()
            {
                Bill = bill,
                BillId = normalCourse.BillId,
                Cost = normalCourse.Cost,
                FoodCourseId = normalCourse.FoodCourseId,
                LastUpdated = normalCourse.LastUpdated,
                Meal = meal,
                MealId = normalCourse.MealId,
                Name = normalCourse.Name,
                Timestamp = normalCourse.Timestamp,
                Type = normalCourse.Type
            };

            return manualCourse;
        }

        public ManualMealEntity ToManualMeal(NormalMealEntity normalMeal, ManualBillEntity bill = null)
        {
            if (normalMeal == null)
            {
                throw new ArgumentNullException("normalMeal");
            }

            if (bill != null && normalMeal.BillId != bill.BillId)
            {
                throw new ArgumentOutOfRangeException("bill");
            }

            var manualMeal = new ManualMealEntity()
            {
                Bill = bill,
                BillId = normalMeal.BillId,
                LastUpdated = normalMeal.LastUpdated,
                MealId = normalMeal.MealId,
                Name = normalMeal.Name,
                Timestamp = normalMeal.Timestamp
            };

            if (normalMeal.Courses != null)
            {
                manualMeal.Courses = normalMeal.Courses.Select(x => ToManualFoodCourse(x, bill, manualMeal)).ToList();
            }

            return manualMeal;
        }

        private static ManualMealEntity FindMeal(IEnumerable<ManualMealEntity> meals, long mealId)
        {
            return meals != null ? meals.FirstOrDefault(x => x.MealId == mealId) : null;
        }

        private static ManualFoodCourseEntity FindFoodCourse(IEnumerable<ManualFoodCourseEntity> courses, long foodCourseId)
        {
            return courses != null ? courses.FirstOrDefault(x => x.FoodCourseId == foodCourseId) : null;
        }
    }
}
