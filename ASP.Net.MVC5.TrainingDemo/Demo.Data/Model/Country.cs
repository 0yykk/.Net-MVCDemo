using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Model
{
    [Table("CountryTable")]
    public class Country
    {
        [Key]
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public IList<State> States { get; set; }
    }
}
