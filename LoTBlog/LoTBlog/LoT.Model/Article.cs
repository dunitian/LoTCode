namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 文章
    /// </summary>
    [Table("Article")]
    public partial class Article
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 标题（最多50个字）
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 作者（最多15个字）
        /// </summary>
        [Required]
        [StringLength(15)]
        public string Author { get; set; }

        /// <summary>
        /// 文章内容（不能为空）
        /// </summary>
        [Required]
        public string TContent { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int HitCount { get; set; }

        /// <summary>
        /// 排序（升序）0在最前面
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 文章类别（资讯1 笔记2 网络资源3）
        /// </summary>
        public LoT.Enums.GroupEnum GroupType { get; set; }

        /// <summary>
        /// 推荐类型 0不推荐 1编辑推荐 2逆天推荐 3网友推荐 4首页置顶
        /// </summary>
        public LoT.Enums.RecommendEnum Recommend { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 分类(,分隔)
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TypeIds { get; set; }

        /// <summary>
        /// SeoId【外键】（将Seo信息放SEOTKD里面）
        /// </summary>
        public int? SeoId { get; set; }

        [ForeignKey("SeoId")]
        public virtual SeoTKD SeoInfo { get; set; }

        /// <summary>
        /// 所属标签（tagid用，分隔）【最多50个字符】
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TagIds { get; set; }

        /// <summary>
        /// 状态（默认为0 0,所有人可见，1,好友可见，2,仅自己可见,99删除）
        /// </summary>
        public LoT.Enums.ArticleStatusEnum Status { get; set; }


        /// <summary>
        /// 默认展图
        /// </summary>
        [Required]
        [StringLength(179)]
        public string DisplayPic { get; set; }
    }
}
