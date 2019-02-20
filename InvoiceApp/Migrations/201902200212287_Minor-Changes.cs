namespace InvoiceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MinorChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.InvoiceItems", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.InvoiceItems", "Product_Id", "dbo.Products");
            DropIndex("dbo.Invoices", new[] { "Customer_Id" });
            DropIndex("dbo.InvoiceItems", new[] { "Invoice_Id" });
            DropIndex("dbo.InvoiceItems", new[] { "Product_Id" });
            RenameColumn(table: "dbo.Invoices", name: "Customer_Id", newName: "CustomerId");
            RenameColumn(table: "dbo.InvoiceItems", name: "Invoice_Id", newName: "InvoiceId");
            RenameColumn(table: "dbo.InvoiceItems", name: "Product_Id", newName: "ProductId");
            AddColumn("dbo.Invoices", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.InvoiceItems", "InvoiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.InvoiceItems", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Invoices", "CustomerId");
            CreateIndex("dbo.InvoiceItems", "InvoiceId");
            CreateIndex("dbo.InvoiceItems", "ProductId");
            AddForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InvoiceItems", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "CustomerId", "dbo.Customers");
            DropIndex("dbo.InvoiceItems", new[] { "ProductId" });
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceId" });
            DropIndex("dbo.Invoices", new[] { "CustomerId" });
            AlterColumn("dbo.InvoiceItems", "ProductId", c => c.Int());
            AlterColumn("dbo.InvoiceItems", "InvoiceId", c => c.Int());
            AlterColumn("dbo.Invoices", "CustomerId", c => c.Int());
            DropColumn("dbo.Invoices", "Amount");
            RenameColumn(table: "dbo.InvoiceItems", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "dbo.InvoiceItems", name: "InvoiceId", newName: "Invoice_Id");
            RenameColumn(table: "dbo.Invoices", name: "CustomerId", newName: "Customer_Id");
            CreateIndex("dbo.InvoiceItems", "Product_Id");
            CreateIndex("dbo.InvoiceItems", "Invoice_Id");
            CreateIndex("dbo.Invoices", "Customer_Id");
            AddForeignKey("dbo.InvoiceItems", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.InvoiceItems", "Invoice_Id", "dbo.Invoices", "Id");
            AddForeignKey("dbo.Invoices", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
