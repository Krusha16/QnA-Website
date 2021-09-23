namespace QnA_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTagClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Tags", new[] { "QuestionId" });
            DropTable("dbo.Tags");
        }
    }
}
