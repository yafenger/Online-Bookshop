namespace Bookshop_CATeam11.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cart")]
    public partial class Cart
    {
        public int CartID { get; set; }

        public int? BookID { get; set; }

        public int? Quantity { get; set; }

        [Required]
        [StringLength(50)]
        public string MemberID { get; set; }
    }
}
