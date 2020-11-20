namespace MeetMeUp_Updated.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingGroupTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        GroupImage = c.String(),
                    })
                .PrimaryKey(t => t.GroupID);
            
            AddColumn("dbo.AspNetUsers", "Group_GroupID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Group_GroupID");
            AddForeignKey("dbo.AspNetUsers", "Group_GroupID", "dbo.Groups", "GroupID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Group_GroupID", "dbo.Groups");
            DropIndex("dbo.AspNetUsers", new[] { "Group_GroupID" });
            DropColumn("dbo.AspNetUsers", "Group_GroupID");
            DropTable("dbo.Groups");
        }
    }
}
