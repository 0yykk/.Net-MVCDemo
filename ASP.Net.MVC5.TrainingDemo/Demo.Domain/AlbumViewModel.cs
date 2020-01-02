using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain
{
    public class AlbumViewModel
    {
        
        public virtual int AlbumId { get; set; }
        public virtual int GenreId { get; set; }
        public virtual int ArtistId { get; set; }
        [Required(ErrorMessage = "Please Input Title")]
        public virtual string Title { get; set; }
        [Required(ErrorMessage = "Please Input Price")]
        public virtual decimal Price { get; set; }
        public virtual string AlbumArtUrl { get; set; }
        public string GenreName { get; set; }
        [Required(ErrorMessage = "Please Input Artist Name")]
        public string ArtistName { get; set; }
        [Required(ErrorMessage = "Please Input Public Date")]
        public virtual DateTime PublicDate { get; set; }
    }
}
