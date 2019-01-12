namespace PizzeriaProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class decimalPrices : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ingredients", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Pizzas", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Pizzas", "SmallPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Pizzas", "MediumPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Pizzas", "LargePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pizzas", "LargePrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Pizzas", "MediumPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Pizzas", "SmallPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Pizzas", "Name", c => c.String());
            AlterColumn("dbo.Ingredients", "Name", c => c.String());
        }
    }
}
