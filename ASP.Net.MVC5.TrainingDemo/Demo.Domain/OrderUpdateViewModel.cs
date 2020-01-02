﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain
{
    public class OrderUpdateViewModel
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public string GenreName { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string OrderGuid { get; set; }
    }
}
