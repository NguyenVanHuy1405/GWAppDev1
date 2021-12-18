namespace GWAppDev1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCourseCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameCourseCategory = c.String(nullable: false, maxLength: 50),
                        Descriptions = c.String(nullable: false, maxLength: 50),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseCategories", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CourseCategories", new[] { "UserId" });
            DropTable("dbo.CourseCategories");
        }
    }
}
