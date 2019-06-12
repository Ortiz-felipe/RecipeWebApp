namespace RecipeBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedRecipeModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipes", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Recipes", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Recipes", "Ingredients", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "Ingredients", c => c.String());
            AlterColumn("dbo.Recipes", "Description", c => c.String());
            AlterColumn("dbo.Recipes", "Name", c => c.String());
        }
    }
}
