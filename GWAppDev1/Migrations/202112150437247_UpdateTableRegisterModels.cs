namespace GWAppDev1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableRegisterModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Staffs", "Fullname", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Staffs", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Staffs", "Address", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Trainees", "Email", c => c.String(nullable: false));
            CreateIndex("dbo.Staffs", "UserId");
            AddForeignKey("dbo.Staffs", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Staffs", new[] { "UserId" });
            AlterColumn("dbo.Trainees", "Email", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Staffs", "Address", c => c.String());
            AlterColumn("dbo.Staffs", "Email", c => c.String());
            AlterColumn("dbo.Staffs", "Fullname", c => c.String());
            DropColumn("dbo.Staffs", "UserId");
        }
    }
}
