using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Model
{
    [Table("NewCityTable")]
    public class City
    {
        [Key]
        public string CityCode { get; set; }
        public string CityName { get; set; }
        [ForeignKey("State")]
        public string StateCode { get; set; }
        public State State { get; set; } 
    }
}
