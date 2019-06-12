namespace RecipeBookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecipeDbSet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.Int(nullable: false),
                        Description = c.String(),
                        Ingredients = c.String(),
                        TotalViews = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Recipes");
        }
    }
}
