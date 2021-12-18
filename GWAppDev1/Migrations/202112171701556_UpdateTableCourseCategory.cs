namespace GWAppDev1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableCourseCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseCategories", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CourseCategories", new[] { "UserId" });
            CreateIndex("dbo.CourseCategories", "NameCourseCategory", unique: true, name: "UniqueNameCourseCategory");
            DropColumn("dbo.CourseCategories", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseCategories", "UserId", c => c.String(maxLength: 128));
            DropIndex("dbo.CourseCategories", "UniqueNameCourseCategory");
            CreateIndex("dbo.CourseCategories", "UserId");
            AddForeignKey("dbo.CourseCategories", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
