namespace GWAppDev1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableCouresCategory : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CourseCategories", "UniqueNameCourseCategory");
        }
        
        public override void Down()
        {
            CreateIndex("dbo.CourseCategories", "NameCourseCategory", unique: true, name: "UniqueNameCourseCategory");
        }
    }
}
