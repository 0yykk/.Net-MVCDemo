using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Model
{
    [Table("Artist")]
    public class Artist
    {
        [Key]
        public virtual int ArtistId { get; set; }
        public virtual string ArtistName { get; set; }
    }
}
