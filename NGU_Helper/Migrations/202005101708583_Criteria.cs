namespace NGU_Helper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criteria : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Criteria",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsActiveForTooltip = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        StatType = c.Int(),
                        Formula = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Criteria");
        }
    }
}
