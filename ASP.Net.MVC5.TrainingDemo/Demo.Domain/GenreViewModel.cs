using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain
{
    public class GenreViewModel
    {
        public virtual int GenreId { get; set; }
        public virtual string GenreName { get; set; }
        public virtual string Description { get; set; }
        public virtual List<AlbumViewModel> Albums { get; set; }
    }
}
