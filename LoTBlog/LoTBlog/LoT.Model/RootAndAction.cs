namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 管理员权限关联表[特色权限表]
    /// </summary>
    [Table("RootAndAction")]
    public partial class RootAndAction
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool IsPass { get; set; }

        /// <summary>
        /// 管理员信息表外键
        /// </summary>
        [Required]
        [StringLength(40)]
        public string UserId { get; set; }

        /// <summary>
        /// 管理员权限表外键
        /// </summary>
        public int ActionInfoId { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
