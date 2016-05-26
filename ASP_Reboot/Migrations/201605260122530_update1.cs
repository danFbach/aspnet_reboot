namespace ASP_Reboot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryModels", "StoreModelsId", c => c.Int(nullable: false));
            Sql(@"UPDATE dbo.InventoryModels SET StoreModelsId = 1 
                where StoreModelsId IS NULL");

        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryModels", "StoreModelsId");
        }
    }
}
