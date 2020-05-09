namespace NGU_Helper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Itemnumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Number");
        }
    }
}
