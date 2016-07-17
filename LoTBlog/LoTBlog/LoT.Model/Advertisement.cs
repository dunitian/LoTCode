namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 广告位表
    /// </summary>
    [Table("Advertisement")]
    public partial class Advertisement
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 广告位置（49字内）
        /// </summary>
        [Required]
        [StringLength(49)]
        public string Map { get; set; }       

        /// <summary>
        /// 内容（HTML标签）
        /// </summary>
        [Required]
        [StringLength(500)]
        public string AContext { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
