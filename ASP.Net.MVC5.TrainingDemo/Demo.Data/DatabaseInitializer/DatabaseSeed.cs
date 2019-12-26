using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Demo.Data.Model;
using Demo.Data.Contexts;
using System.Data.SqlClient;

namespace Demo.Data.DatabaseInitializer
{
    public class DatabaseSeed
    {
        public static void SeedDatabase(MusicStoreContext context)
        {
            var GenreList = new List<Genre>
            {
                new Genre{GenreId=001,GenreName="Hip-Pop",Description="Popular"}
            };
            var ArtistList = new List<Artist>
            {
                new Artist{ArtistId=00001,ArtistName="Kendrick Lamar"}
            };
            var AlbumList = new List<Album>
            {
                new Album{AlbumId=00001,AlbumArtUrl="www.google.com",ArtistName="Kendrick Lamar",GenreName="Hip-Pop",Title="HUMBLE",Price=10,PublicDate=DateTime.Now,GenreId=001, ArtistId=00001}, 
            };
            GenreList.ForEach(s => context.Genre.Add(s));
            ArtistList.ForEach(s => context.Artist.Add(s));
            AlbumList.ForEach(s => context.Album.Add(s));
            context.SaveChanges();  
        }
        public static void SeedDatabaseOrder(MusicStoreContext db)
        {
            var _Order = new Order()
            {
                OrderGuid = "12345",
                OrderDate = DateTime.Now,
                UserName = "1234",
                FirstName = "123345",
                LastName = "12345",
                Address = "123214",
                City = "12345",
                State = "1234",
                PostalCode = "1234",
                Country = "123",
                Phone = "1234",
                Email = "1234",
                TotalPrice = 123
            };
            db.Order.Add(_Order);
            db.SaveChanges();
        }

    }
}
