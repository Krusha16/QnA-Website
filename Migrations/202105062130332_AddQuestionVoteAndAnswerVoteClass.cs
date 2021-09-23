namespace QnA_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuestionVoteAndAnswerVoteClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerVotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteType = c.Int(nullable: false),
                        AnswerId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.AnswerId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.QuestionVotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteType = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.Questions", "VoteType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "VoteType", c => c.Int(nullable: false));
            DropForeignKey("dbo.QuestionVotes", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionVotes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AnswerVotes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AnswerVotes", "AnswerId", "dbo.Answers");
            DropIndex("dbo.QuestionVotes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.QuestionVotes", new[] { "QuestionId" });
            DropIndex("dbo.AnswerVotes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AnswerVotes", new[] { "AnswerId" });
            DropTable("dbo.QuestionVotes");
            DropTable("dbo.AnswerVotes");
        }
    }
}
