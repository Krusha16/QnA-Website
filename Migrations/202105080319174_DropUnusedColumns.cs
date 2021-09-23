namespace QnA_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropUnusedColumns : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Answers", "NumOfVotes");
            DropColumn("dbo.Answers", "VoteType");
            DropColumn("dbo.Questions", "NumOfVotes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "NumOfVotes", c => c.Int(nullable: false));
            AddColumn("dbo.Answers", "VoteType", c => c.Int(nullable: false));
            AddColumn("dbo.Answers", "NumOfVotes", c => c.Int(nullable: false));
        }
    }
}
