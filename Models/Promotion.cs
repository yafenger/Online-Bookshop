namespace Bookshop_CATeam11.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Promotion")]
    public partial class Promotion
    {
        public int PromotionID { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Discount must be between 1 and 100")]
        public decimal Discount_Price { get; set; }
        [Required(ErrorMessage = "Start Date field is required")]
        public DateTime Start_Time { get; set; }
        [Required(ErrorMessage = "End Date field is required")]
        [DateGreaterThanAttribute(Start_Time = "Start_Time", ErrorMessage = "End Date must be after Start Date")]
        public DateTime End_Time { get; set; }
    }
}
