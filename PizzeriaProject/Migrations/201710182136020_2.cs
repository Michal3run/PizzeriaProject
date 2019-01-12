namespace PizzeriaProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pizzas", "SmallPrice", c => c.Int(nullable: false));
            AddColumn("dbo.Pizzas", "MediumPrice", c => c.Int(nullable: false));
            AddColumn("dbo.Pizzas", "LargePrice", c => c.Int(nullable: false));
            DropColumn("dbo.Pizzas", "Prices_Small");
            DropColumn("dbo.Pizzas", "Prices_Medium");
            DropColumn("dbo.Pizzas", "Prices_Large");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pizzas", "Prices_Large", c => c.Int(nullable: false));
            AddColumn("dbo.Pizzas", "Prices_Medium", c => c.Int(nullable: false));
            AddColumn("dbo.Pizzas", "Prices_Small", c => c.Int(nullable: false));
            DropColumn("dbo.Pizzas", "LargePrice");
            DropColumn("dbo.Pizzas", "MediumPrice");
            DropColumn("dbo.Pizzas", "SmallPrice");
        }
    }
}
