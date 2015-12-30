namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bill",
                c => new
                    {
                        BillId = c.Long(nullable: false, identity: true),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BillId);
            
            CreateTable(
                "dbo.FoodCourse",
                c => new
                    {
                        FoodCourseId = c.Long(nullable: false, identity: true),
                        MealId = c.Long(nullable: false),
                        BillId = c.Long(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false),
                        Name = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.FoodCourseId)
                .ForeignKey("dbo.Bill", t => t.BillId, cascadeDelete: true)
                .ForeignKey("dbo.Meal", t => t.MealId, cascadeDelete: true)
                .Index(t => t.MealId)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.Meal",
                c => new
                    {
                        MealId = c.Long(nullable: false, identity: true),
                        BillId = c.Long(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        LastUpdated = c.DateTime(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MealId)
                .ForeignKey("dbo.Bill", t => t.BillId, cascadeDelete: true)
                .Index(t => t.BillId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FoodCourse", "MealId", "dbo.Meal");
            DropForeignKey("dbo.Meal", "BillId", "dbo.Bill");
            DropForeignKey("dbo.FoodCourse", "BillId", "dbo.Bill");
            DropIndex("dbo.Meal", new[] { "BillId" });
            DropIndex("dbo.FoodCourse", new[] { "BillId" });
            DropIndex("dbo.FoodCourse", new[] { "MealId" });
            DropTable("dbo.Meal");
            DropTable("dbo.FoodCourse");
            DropTable("dbo.Bill");
        }
    }
}
