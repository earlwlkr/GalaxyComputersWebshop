namespace GalaxyComputersASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateManufacturer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        WebPage = c.String(),
                        ImagePath = c.String(),
                        Country = c.String(),
                        DateFounded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Products", "ManufacturerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "ManufacturerID");
            AddForeignKey("dbo.Products", "ManufacturerID", "dbo.Manufacturers", "ID", cascadeDelete: true);
            DropColumn("dbo.Products", "Brand");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Brand", c => c.String());
            DropForeignKey("dbo.Products", "ManufacturerID", "dbo.Manufacturers");
            DropIndex("dbo.Products", new[] { "ManufacturerID" });
            DropColumn("dbo.Products", "ManufacturerID");
            DropTable("dbo.Manufacturers");
        }
    }
}
