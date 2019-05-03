namespace Bookshop_CATeam11.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        public int BookID { get; set; }

        [Required]
        [StringLength(120)]
        public string Title { get; set; }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(22)]
        public string ISBN { get; set; }

        [Required]
        [StringLength(64)]
        public string Author { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        [Column(TypeName = "image")]
        public byte[] Cover { get; set; }

        [NotMapped]
        public List<Category> CategoryCollection { get; set; }
    }
}
