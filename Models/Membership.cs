namespace Bookshop_CATeam11.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Membership")]
    public partial class Membership
    {
        [Key]
        [StringLength(50)]
        public string MemberID { get; set; }

        [StringLength(50)]
        public string CustomerName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
    }
}
