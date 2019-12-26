namespace Demo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetail", "nowTotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Order", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "TotalPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetail", "nowTotalPrice", c => c.Int(nullable: false));
        }
    }
}
