namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 管理员信息表
    /// </summary>
    [Table("DntRootInfo")]
    public partial class DntRootInfo
    {
        /// <summary>
        /// 编号（Guid~防止顺序解猜）
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// 昵称（最多15个字）
        /// </summary>
        [Required]
        [StringLength(15)]
        public string NoName { get; set; }

        /// <summary>
        /// 密码（SHA加密40位）
        /// </summary>
        [Required]
        [StringLength(40)]
        public string NoPass { get; set; }

        /// <summary>
        /// 状态（0 正常，1 冻结，99删除）
        /// </summary>
        public LoT.Enums.AdminEnum Status { get; set; }
    }
}
