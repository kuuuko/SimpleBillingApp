namespace SimpleBillingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "customerAdress", c => c.String());
            AddColumn("dbo.Customers", "customerContact", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "customerContact");
            DropColumn("dbo.Customers", "customerAdress");
        }
    }
}
