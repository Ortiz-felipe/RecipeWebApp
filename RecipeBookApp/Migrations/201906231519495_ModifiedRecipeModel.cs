namespace RecipeBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedRecipeModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "UserName", c => c.String());
            AlterColumn("dbo.Recipes", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Recipes", "UserName");
        }
    }
}
