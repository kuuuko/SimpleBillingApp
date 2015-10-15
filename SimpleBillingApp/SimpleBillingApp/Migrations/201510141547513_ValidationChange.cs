namespace SimpleBillingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "customerName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "customerAdress", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "customerContact", c => c.String(nullable: false));
            AlterColumn("dbo.Articles", "articleName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "articleName", c => c.String());
            AlterColumn("dbo.Customers", "customerContact", c => c.String());
            AlterColumn("dbo.Customers", "customerAdress", c => c.String());
            AlterColumn("dbo.Customers", "customerName", c => c.String());
        }
    }
}
