using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Enums
{
    /// <summary>
    /// 危害等级
    /// </summary>
    public enum HarmLevelEnum
    {
        /// <summary>
        /// 正常-安全
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 轻微-子弹
        /// </summary>
        Bullet = 1,

        /// <summary>
        /// 中等-导弹
        /// </summary>
        Missiles = 2,

        /// <summary>
        /// 严重-核弹
        /// </summary>
        Nuke = 3
    }
}
