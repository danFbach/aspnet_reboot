namespace ASP_Reboot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.InventoryModels", "StoreModelsId");
            AddForeignKey("dbo.InventoryModels", "StoreModelsId", "dbo.StoreModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryModels", "StoreModelsId", "dbo.StoreModels");
            DropIndex("dbo.InventoryModels", new[] { "StoreModelsId" });
        }
    }
}
