namespace GalaxyComputersASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateComments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "ManufacturerID", "dbo.Products");
            DropIndex("dbo.Comments", new[] { "ManufacturerID" });
            RenameColumn(table: "dbo.Comments", name: "ManufacturerID", newName: "Product_ID");
            AlterColumn("dbo.Comments", "Product_ID", c => c.Int());
            CreateIndex("dbo.Comments", "Product_ID");
            AddForeignKey("dbo.Comments", "Product_ID", "dbo.Products", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Product_ID", "dbo.Products");
            DropIndex("dbo.Comments", new[] { "Product_ID" });
            AlterColumn("dbo.Comments", "Product_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Comments", name: "Product_ID", newName: "ManufacturerID");
            CreateIndex("dbo.Comments", "ManufacturerID");
            AddForeignKey("dbo.Comments", "ManufacturerID", "dbo.Products", "ID", cascadeDelete: true);
        }
    }
}
