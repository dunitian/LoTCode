namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 文章默认展图
    /// </summary>
    [Table("ArticleDisPhoto")]
    public partial class ArticleDisPhoto
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 标题（鼠标放头像上显示的alt，15个字以内）
        /// </summary>
        [Required]
        [StringLength(15)]
        public string Title { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [Required]
        [StringLength(179)]
        public string PicUrl { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
