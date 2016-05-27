namespace ASP_Reboot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reboot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoreModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        city = c.String(nullable: false),
                        address = c.String(nullable: false),
                        zipcode = c.Int(nullable: false),
                        geoLat = c.Double(nullable: false),
                        getLong = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StoreModels");
        }
    }
}
