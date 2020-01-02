using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain
{
    public class OrderViewModel
    {
        public string OrderGuid { get; set; }
        public DateTime? OrderDate { get; set; }
        [StringLength(12, ErrorMessage = "too long")]
        [Required(ErrorMessage = "Please Input UserName")]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Input Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please select your City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please select your State")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please Input PastalCode")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Please select your Country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please Input Phone")]
        [RegularExpression(@"\d{11}", ErrorMessage = "Error Phone")]
        public string Phone { get; set; }
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$", ErrorMessage = "Error Email")]
        public string Email { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
