namespace NGU_Helper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "ItemType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "ItemType");
        }
    }
}
