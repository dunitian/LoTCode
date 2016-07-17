namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// SeoTKD表
    /// </summary>
    [Table("SeoTKD")]
    public partial class SeoTKD
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// SEO关键词（，分隔 149字以内）
        /// </summary>
        [Required]
        [StringLength(149)]
        public string SeoKeywords { get; set; }

        /// <summary>
        /// SEO内容（249字以内）
        /// </summary>
        [Required]
        [StringLength(249)]
        public string Sedescription { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
