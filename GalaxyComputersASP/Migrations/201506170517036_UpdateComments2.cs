namespace GalaxyComputersASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateComments2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "User_Id", newName: "UserID");
            RenameIndex(table: "dbo.Comments", name: "IX_User_Id", newName: "IX_UserID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_UserID", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Comments", name: "UserID", newName: "User_Id");
        }
    }
}
