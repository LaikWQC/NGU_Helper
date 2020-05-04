namespace NGU_Helper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Zones",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Level = c.Int(nullable: false),
                        ZoneId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Zones", t => t.ZoneId, cascadeDelete: true)
                .Index(t => t.ZoneId);
            
            CreateTable(
                "dbo.Stats",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ItemId = c.Guid(nullable: false),
                        StatType = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "ZoneId", "dbo.Zones");
            DropForeignKey("dbo.Stats", "ItemId", "dbo.Items");
            DropIndex("dbo.Stats", new[] { "ItemId" });
            DropIndex("dbo.Items", new[] { "ZoneId" });
            DropTable("dbo.Stats");
            DropTable("dbo.Items");
            DropTable("dbo.Zones");
        }
    }
}
