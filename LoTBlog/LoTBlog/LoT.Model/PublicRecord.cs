namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 访客记录表
    /// </summary>
    [Table("PublicRecord")]
    public partial class PublicRecord
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 游客地区
        /// </summary>
        [Required]
        [StringLength(39)]
        public string Address { get; set; }

        /// <summary>
        /// 访客的IP地址（最多64位）
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Ip { get; set; }

        /// <summary>
        /// 状态（后期可以真删）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
