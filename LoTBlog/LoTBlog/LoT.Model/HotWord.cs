namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 热词表（用户搜的热词）
    /// </summary>
    [Table("HotWord")]
    public partial class HotWord
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 名称（25字内）
        /// </summary>
        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        /// <summary>
        /// 次数
        /// </summary>
        public int HotCount { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
