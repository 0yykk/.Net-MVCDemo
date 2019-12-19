using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Data.Model
{
    public class OrderDetail
    {
        [Key]
        public string OrderDetailGuid { get; set; }
        [ForeignKey("Album")]
        public int AlbumId { get; set; }
        public int Total { get; set; }
        [ForeignKey("Order")]
        public string OrderGuid { get; set; }
        public int nowTotalPrice { get; set; }
    }
}
