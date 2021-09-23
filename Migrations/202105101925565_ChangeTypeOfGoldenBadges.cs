namespace QnA_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeOfGoldenBadges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "GoldenBadges", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "HasGoldenBadge");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "HasGoldenBadge", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "GoldenBadges");
        }
    }
}
