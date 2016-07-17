using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Enums
{
    public enum StatusEnum
    {
        /// <summary>
        /// 待审核状态（0）默认
        /// </summary>
        Pendding = 0,

        /// <summary>
        /// 审核已通过（1）正常
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 审核不通过（2）未删
        /// </summary>
        Cancel = 2,

        /// <summary>
        /// 已删除状态（99）假删
        /// </summary>
        Delete = 99
    }
}
