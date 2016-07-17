namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 相册
    /// </summary>
    [Table("Photo")]
    public partial class Photo
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 标题（15字内）
        /// </summary>
        [Required]
        [StringLength(15)]
        public string Title { get; set; }

        /// <summary>
        /// 相册展示图地址
        /// </summary>
        [Required]
        [StringLength(179)]
        public string DisPlayPic { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }

        /// <summary>
        /// 排序（降序排列）
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 相册类别【外键】
        /// </summary>
        public int PTypeId { get; set; }
    }
}
