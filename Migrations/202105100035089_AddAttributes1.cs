namespace QnA_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttributes1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "Title", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "Title", c => c.String());
        }
    }
}
