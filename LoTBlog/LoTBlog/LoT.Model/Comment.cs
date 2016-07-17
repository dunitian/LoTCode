namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 评论表
    /// </summary>
    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 昵称（最多15个字）
        /// </summary>
        [Required]
        [StringLength(15)]
        public string Nickname { get; set; }

        /// <summary>
        /// 标题【可空】（最多25个字）
        /// </summary>
        [StringLength(25)]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public string Context { get; set; }

        /// <summary>
        /// 地区（通过API得到）
        /// </summary>
        [Required]
        [StringLength(25)]
        public string Address { get; set; }

        /// <summary>
        /// IP地址（最多64位）
        /// </summary>
        [Required]
        [StringLength(64)]
        public string IPAddress { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
