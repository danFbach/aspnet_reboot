namespace ASP_Reboot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inventory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SKU = c.Int(nullable: false),
                        productName = c.String(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InventoryModels");
        }
    }
}
