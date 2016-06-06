namespace ASP_Reboot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addwarningandrefilllevelstoinventory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryModels", "warningLevel", c => c.Int(nullable: false));
            AddColumn("dbo.InventoryModels", "refillLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryModels", "refillLevel");
            DropColumn("dbo.InventoryModels", "warningLevel");
        }
    }
}
