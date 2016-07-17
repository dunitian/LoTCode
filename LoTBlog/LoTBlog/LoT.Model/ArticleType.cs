namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 文章分类表
    /// </summary>
    [Table("ArticleType")]
    public partial class ArticleType
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 类型名（最多20个字）
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int? Pid { get; set; }

        /// <summary>
        /// 父分类
        /// </summary>
        [ForeignKey("Pid")]
        public virtual ArticleType ParentType { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
