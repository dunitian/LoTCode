using System;
using System.Collections.Generic;
namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// QQModel表
    /// </summary>
    [Table("QQModel")]
    public partial class QQModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 昵称（最多100个字）
        /// </summary>
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        [StringLength(200)]
        public string OpenId { get; set; }

        /// <summary>
        /// AccessToken
        /// </summary>
        [StringLength(200)]
        public string AccessToken { get; set; }

        /// <summary>
        /// Figureurl 头像url
        /// </summary>
        [StringLength(200)]
        public string Figureurl { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime EndDataTime { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 状态（0 正常，1 冻结，99删除）
        /// </summary>
        public LoT.Enums.AdminEnum Status { get; set; }
    }
}
