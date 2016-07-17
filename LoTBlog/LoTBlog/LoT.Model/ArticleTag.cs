namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Tag表（系统的一些分类【一个文章可以有多个标签】）
    /// </summary>
    [Table("ArticleTag")]
    public partial class ArticleTag
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 名称（15字以内）
        /// </summary>
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
