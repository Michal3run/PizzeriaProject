namespace PizzeriaProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ingred : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ingredients", "IsForAllPizzas");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingredients", "IsForAllPizzas", c => c.Boolean(nullable: false));
        }
    }
}
