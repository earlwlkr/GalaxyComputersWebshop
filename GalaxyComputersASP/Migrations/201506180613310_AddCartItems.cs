namespace GalaxyComputersASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        UserID = c.String(),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "ProductID", "dbo.Products");
            DropIndex("dbo.CartItems", new[] { "ProductID" });
            DropTable("dbo.CartItems");
        }
    }
}
