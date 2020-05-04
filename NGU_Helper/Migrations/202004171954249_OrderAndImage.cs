namespace NGU_Helper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderAndImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zones", "Order", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "ImageUrl");
            DropColumn("dbo.Zones", "Order");
        }
    }
}
