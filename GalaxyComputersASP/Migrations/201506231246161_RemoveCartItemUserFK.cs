namespace GalaxyComputersASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCartItemUserFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.CartItems", new[] { "UserID" });
            AlterColumn("dbo.CartItems", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CartItems", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.CartItems", "UserID");
            AddForeignKey("dbo.CartItems", "UserID", "dbo.AspNetUsers", "Id");
        }
    }
}
