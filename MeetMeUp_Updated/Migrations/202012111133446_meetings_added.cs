namespace MeetMeUp_Updated.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class meetings_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meetings",
                c => new
                    {
                        MeetingID = c.Int(nullable: false, identity: true),
                        GroupID = c.Int(nullable: false),
                        Title = c.String(),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MeetingID)
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meetings", "GroupID", "dbo.Groups");
            DropIndex("dbo.Meetings", new[] { "GroupID" });
            DropTable("dbo.Meetings");
        }
    }
}
