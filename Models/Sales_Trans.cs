namespace Bookshop_CATeam11.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sales_Trans
    {
        [Key]
        public int SalesID { get; set; }

        public int? PromotionID { get; set; }

        public DateTime SalesDate { get; set; }

        public int? Quantity { get; set; }

        public decimal? Total_Amount { get; set; }

        public int BookID { get; set; }

        [Required]
        [StringLength(50)]
        public string MemberID { get; set; }
    }
}
