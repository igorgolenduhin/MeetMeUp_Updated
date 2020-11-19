namespace MeetMeUp_Updated.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kek : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserImg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserImg");
        }
    }
}
