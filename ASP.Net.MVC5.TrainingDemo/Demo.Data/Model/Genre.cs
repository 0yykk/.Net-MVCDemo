using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Model
{
    [Table("Genre")]
    public class Genre
    {
        [Key]
        public  int GenreId { get; set; }
        public  string GenreName { get; set; }
        public  string Description { get; set; }
        public  IList<Album> Albums { get; set; }
    }
}
