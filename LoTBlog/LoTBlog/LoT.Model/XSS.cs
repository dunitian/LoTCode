namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 统计表，缓解DDos攻击
    /// 如果距离上一次访问时间不足1h，则不更新上一次访问时间，并且在DDos Cout列中+1
    //  当Count列大于100时=>限制（验证）当大于200时=>拒绝（阉割所有请求）当大于500时=>反击！
    //  当时间间隔大于1h，则把当前时间写入上一次访问时间表中，并把Count列清零（防止误攻击）
    /// </summary>
    [Table("XSS")]
    public partial class XSS
    {
        /// <summary>
        /// 编号（Guid~防止顺序解猜）
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// 访客的IP地址（最多64位）
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Ip { get; set; }

        /// <summary>
        /// XSS记录总数（大于50=>警告，大于100=>X死他）
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 上一次的访问时间（时间间隔不足1h不更新）
        /// </summary>
        [Required]
        public DateTime LastTime { get; set; }

        /// <summary>
        /// 最后一次访问时间
        /// </summary>
        [Required]
        public string EndTime { get; set; }

        /// <summary>
        /// 状态（以后可以真删）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
