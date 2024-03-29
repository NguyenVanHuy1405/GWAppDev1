﻿namespace GWAppDev1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CourseTrainers", name: "TrainerId", newName: "UserId");
            RenameIndex(table: "dbo.CourseTrainers", name: "IX_TrainerId", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CourseTrainers", name: "IX_UserId", newName: "IX_TrainerId");
            RenameColumn(table: "dbo.CourseTrainers", name: "UserId", newName: "TrainerId");
        }
    }
}
