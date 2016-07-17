namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 幻灯片表
    /// </summary>
    [Table("ImgFlash")]
    public partial class ImgFlash
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 上标题（不超过100个字）
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TopTitle { get; set; }

        /// <summary>
        /// 下标题（不超过100个字）
        /// </summary>
        [Required]
        [StringLength(100)]
        public string BottomTitle { get; set; }

        /// <summary>
        /// 背景图（179字符以内,推荐715 * 290）
        /// </summary>
        [Required]
        [StringLength(500)]
        public string BackImg { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
