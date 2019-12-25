using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.Net.MVC5.TrainingDemo.Models
{
    public class CartListView
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public string GenreName { get; set; }
        public string Price { get; set; }
    }
}