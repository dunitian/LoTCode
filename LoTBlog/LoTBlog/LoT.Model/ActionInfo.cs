namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 管理员权限表
    /// </summary>
    [Table("ActionInfo")]
    public partial class ActionInfo
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 权限名
        /// </summary>
        [Required]
        [StringLength(30)]
        public string ActionName { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Url { get; set; }

        /// <summary>
        /// Http状态（post get）
        /// </summary>
        public int HttpMethod { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime SubTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 权限状态
        /// </summary>
        public LoT.Enums.StatusEnum Statu { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public int ActionInfoType { get; set; }
    }
}
