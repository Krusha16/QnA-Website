namespace QnA_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVoteTypeEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "VoteType", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "VoteType", c => c.Int(nullable: false));
            DropColumn("dbo.Answers", "IsUpvoted");
            DropColumn("dbo.Questions", "IsUpvoted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "IsUpvoted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Answers", "IsUpvoted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Questions", "VoteType");
            DropColumn("dbo.Answers", "VoteType");
        }
    }
}
