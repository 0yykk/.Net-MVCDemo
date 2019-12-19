using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain
{
    public class AlbumViewModel
    {
        public virtual int AlbunmId { get; set; }
        public virtual int GenreId { get; set; }
        public virtual int ArtistId { get; set; }
        public virtual string Title { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string AlbumArtUrl { get; set; }
        public virtual GenreViewModel Genre { get; set; }
        public virtual ArtistViewModel Artist { get; set; }
        public virtual DateTime PublicDate { get; set; }
    }
}
