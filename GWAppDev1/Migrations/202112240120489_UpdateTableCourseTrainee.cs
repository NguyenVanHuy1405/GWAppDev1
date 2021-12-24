namespace GWAppDev1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableCourseTrainee : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CourseTrainees", name: "TraineeId", newName: "UserId");
            RenameIndex(table: "dbo.CourseTrainees", name: "IX_TraineeId", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CourseTrainees", name: "IX_UserId", newName: "IX_TraineeId");
            RenameColumn(table: "dbo.CourseTrainees", name: "UserId", newName: "TraineeId");
        }
    }
}
