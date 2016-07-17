using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Enums
{
    public enum AdminEnum
    {
        // <summary>
        /// 正常模式（1）正常
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 冷冻模式（2）未删
        /// </summary>
        Cancel = 2,

        /// <summary>
        /// 游客模式（3）未删
        /// </summary>
        Temp = 3,

        /// <summary>
        /// 已删除状态（99）假删
        /// </summary>
        Delete = 99
    }
}
