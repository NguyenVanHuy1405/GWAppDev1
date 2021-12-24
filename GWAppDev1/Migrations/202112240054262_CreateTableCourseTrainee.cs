namespace GWAppDev1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCourseTrainee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseTrainees",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        TraineeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CourseId, t.TraineeId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.TraineeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseTrainees", "TraineeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseTrainees", "CourseId", "dbo.Courses");
            DropIndex("dbo.CourseTrainees", new[] { "TraineeId" });
            DropIndex("dbo.CourseTrainees", new[] { "CourseId" });
            DropTable("dbo.CourseTrainees");
        }
    }
}
