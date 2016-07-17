namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 友情链接
    /// </summary>
    [Table("FriendLink")]
    public partial class FriendLink
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 链接名（15字以内）
        /// </summary>
        [Required]
        [StringLength(15)]
        public string LinkName { get; set; }

        /// <summary>
        /// 网站地址（50字符以内）
        /// </summary>
        [Required]
        [StringLength(50)]
        public string WebUrl { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }

        /// <summary>
        /// 排序（降序排列）
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 所属分类
        /// </summary>
        public LoT.Enums.LinkTypeEnum LinkType { get; set; }
    }
}
