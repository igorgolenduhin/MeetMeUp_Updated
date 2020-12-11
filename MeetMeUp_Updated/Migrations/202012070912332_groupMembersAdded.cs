namespace MeetMeUp_Updated.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groupMembersAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Group_GroupID", "dbo.Groups");
            DropIndex("dbo.AspNetUsers", new[] { "Group_GroupID" });
            CreateTable(
                "dbo.ApplicationUserGroups",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Group_GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Group_GroupID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_GroupID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Group_GroupID);
            
            DropColumn("dbo.AspNetUsers", "Group_GroupID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Group_GroupID", c => c.Int());
            DropForeignKey("dbo.ApplicationUserGroups", "Group_GroupID", "dbo.Groups");
            DropForeignKey("dbo.ApplicationUserGroups", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserGroups", new[] { "Group_GroupID" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserGroups");
            CreateIndex("dbo.AspNetUsers", "Group_GroupID");
            AddForeignKey("dbo.AspNetUsers", "Group_GroupID", "dbo.Groups", "GroupID");
        }
    }
}
