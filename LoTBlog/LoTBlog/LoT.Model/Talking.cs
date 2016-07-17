namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 说说+日常生活（单独一个表，是不推荐给用户看的）
    /// </summary>
    [Table("Talking")]
    public partial class Talking
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 标题（最多25个字）
        /// </summary>
        [StringLength(25)]
        public string Title { get; set; }

        /// <summary>
        /// 内容（最多500个字）
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Say { get; set; }

        /// <summary>
        /// 展览图（默认图：前台在默认图库里面随机取一张）
        /// </summary>
        [Required]
        [StringLength(179)]
        public string DisplayPic { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间（第一次和创建时间是一样的）
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public int HitCount { get; set; }

        /// <summary>
        /// 状态（默认为0 0,所有人可见，1,好友可见，2,仅自己可见,99删除）
        /// </summary>
        public LoT.Enums.ArticleStatusEnum Status { get; set; }
    }
}
