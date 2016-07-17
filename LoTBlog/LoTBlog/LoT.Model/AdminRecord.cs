namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 管理员操作记录表
    /// </summary>
    [Table("AdminRecord")]
    public partial class AdminRecord
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 管理员昵称
        /// </summary>
        [Required]
        [StringLength(15)]
        public string Nickname { get; set; }

        /// <summary>
        /// 访客的IP地址（最多64位）
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Ip { get; set; }

        /// <summary>
        /// 他的操作（249字内）
        /// </summary>
        [Required]
        [StringLength(249)]
        public string Action { get; set; }

        /// <summary>
        /// 状态（后期可以真删）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
