namespace RecipeBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableTotalViews : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipes", "TotalViews", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "TotalViews", c => c.Int(nullable: false));
        }
    }
}
