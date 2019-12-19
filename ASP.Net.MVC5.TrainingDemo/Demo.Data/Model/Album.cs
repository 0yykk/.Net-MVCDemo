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
        public virtual int AlbumId { get; set; }
        public virtual int GenreId { get; set; }
        public virtual int ArtistId { get; set; }
        public virtual string Title { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string AlbumArtUrl  { get; set; }
        [ForeignKey("Genre")]
        public string GenreName { get; set; }
        public virtual Genre Genre { get; set; }
        [ForeignKey("Artist")]
        public string ArtistName { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual DateTime PublicDate { get; set; }
    }
}
