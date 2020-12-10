namespace The_Pet_Mansion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        AnimalID = c.Int(nullable: false, identity: true),
                        Species = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AnimalID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Stock = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        AnimalID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Animals", t => t.AnimalID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.CategoryID)
                .Index(t => t.AnimalID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.CartLines",
                c => new
                    {
                        CartLineID = c.Int(nullable: false, identity: true),
                        CartID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Cart_CartID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CartLineID)
                .ForeignKey("dbo.Carts", t => t.Cart_CartID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.Cart_CartID);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CartID)
                .ForeignKey("dbo.AspNetUsers", t => t.CartID)
                .Index(t => t.CartID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        OrderLineID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderLineID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingID = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false, maxLength: 128),
                        ProductID = c.String(nullable: false),
                        Value = c.Int(nullable: false),
                        Product_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.RatingID)
                .ForeignKey("dbo.Products", t => t.Product_ProductID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.Product_ProductID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ProductID = c.String(),
                        UserID = c.String(maxLength: 128),
                        Product_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Products", t => t.Product_ProductID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.Product_ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "Product_ProductID", "dbo.Products");
            DropForeignKey("dbo.Ratings", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "Product_ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderLines", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Orders", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderLines", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.CartLines", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Carts", "CartID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CartLines", "Cart_CartID", "dbo.Carts");
            DropForeignKey("dbo.Products", "AnimalID", "dbo.Animals");
            DropIndex("dbo.Reviews", new[] { "Product_ProductID" });
            DropIndex("dbo.Reviews", new[] { "UserID" });
            DropIndex("dbo.Ratings", new[] { "Product_ProductID" });
            DropIndex("dbo.Ratings", new[] { "UserID" });
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropIndex("dbo.OrderLines", new[] { "ProductID" });
            DropIndex("dbo.OrderLines", new[] { "OrderID" });
            DropIndex("dbo.Carts", new[] { "CartID" });
            DropIndex("dbo.CartLines", new[] { "Cart_CartID" });
            DropIndex("dbo.CartLines", new[] { "ProductID" });
            DropIndex("dbo.Products", new[] { "UserID" });
            DropIndex("dbo.Products", new[] { "AnimalID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Ratings");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderLines");
            DropTable("dbo.Categories");
            DropTable("dbo.Carts");
            DropTable("dbo.CartLines");
            DropTable("dbo.Products");
            DropTable("dbo.Animals");
        }
    }
}
