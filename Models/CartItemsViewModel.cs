using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshop_CATeam11.Models
{
    public class CartItemsViewModel
    {
        public int CartID { get; set; }
        public int BookID { get; set; }
        public string MemberID { get; set; }
        public string Title { get; set; }
        public byte[] Cover { get; set; }
        public decimal? Price { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public int? Quantity { get; set; }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}