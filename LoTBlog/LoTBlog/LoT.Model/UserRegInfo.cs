namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// ≤‚ ‘¿‡~”’∆≠◊®”√
    /// </summary>
    [Table("UserRegInfo")]
    public partial class UserRegInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Pass { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
