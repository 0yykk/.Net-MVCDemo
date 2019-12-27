using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Model
{
    [Table("StateTable")]
    public class State
    {
        [Key]
        public string StateCode { get; set; }
        public string StateName { get; set; }
        [ForeignKey("Country")]
        public string CountryCode { get; set; }
        public Country Country { get; set; }
        public IList<City> Cities { get; set; }
    }
}
