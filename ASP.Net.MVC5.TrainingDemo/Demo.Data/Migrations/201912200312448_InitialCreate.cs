namespace Demo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        AlbumId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AlbumArtUrl = c.String(),
                        GenreName = c.String(),
                        GenreId = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                        ArtistName = c.String(),
                        PublicDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumId)
                .ForeignKey("dbo.Artist", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Genre", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Artist",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        ArtistName = c.String(),
                    })
                .PrimaryKey(t => t.ArtistId);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        GenreName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        OrderDetailGuid = c.String(nullable: false, maxLength: 128),
                        AlbumId = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                        OrderGuid = c.String(maxLength: 128),
                        nowTotalPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailGuid)
                .ForeignKey("dbo.Album", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderGuid)
                .Index(t => t.AlbumId)
                .Index(t => t.OrderGuid);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderGuid = c.String(nullable: false, maxLength: 128),
                        OrderDate = c.DateTime(nullable: false),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        TotalPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderGuid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetail", "OrderGuid", "dbo.Order");
            DropForeignKey("dbo.OrderDetail", "AlbumId", "dbo.Album");
            DropForeignKey("dbo.Album", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.Album", "ArtistId", "dbo.Artist");
            DropIndex("dbo.OrderDetail", new[] { "OrderGuid" });
            DropIndex("dbo.OrderDetail", new[] { "AlbumId" });
            DropIndex("dbo.Album", new[] { "ArtistId" });
            DropIndex("dbo.Album", new[] { "GenreId" });
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Genre");
            DropTable("dbo.Artist");
            DropTable("dbo.Album");
        }
    }
}
