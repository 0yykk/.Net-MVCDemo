using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Model
{
    [Table("Album")]
    public class Album
    {
        [Key]
        public  int AlbumId { get; set; }
        public  string Title { get; set; }
        public  decimal Price { get; set; }
        public  string AlbumArtUrl  { get; set; }
        public string GenreName { get; set; }
        [ForeignKey("Genre")]
        public  int GenreId { get; set; }
        public  Genre Genre { get; set; }
        [ForeignKey("Artist")] 
        public  int ArtistId { get; set; }
        public  Artist Artist { get; set; }
        public string ArtistName { get; set; }
        public  DateTime PublicDate { get; set; }
    }
}
