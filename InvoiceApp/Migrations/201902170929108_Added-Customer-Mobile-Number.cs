namespace InvoiceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCustomerMobileNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "MobileNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "MobileNumber");
        }
    }
}
