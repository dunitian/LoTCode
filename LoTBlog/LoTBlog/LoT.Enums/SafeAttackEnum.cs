using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Enums
{
    /// <summary>
    /// 攻击类型
    /// </summary>
    public enum SafeAttackEnum
    {
        // <summary>
        /// XSS攻击
        /// </summary>
        XSS = 1,

        /// <summary>
        /// DDos攻击
        /// </summary>
        DDos = 2,

        /// <summary>
        /// 其他攻击
        /// </summary>
        Other = 9
    }
}
