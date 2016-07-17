using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Enums
{
    public enum ArticleStatusEnum
    {
        /// <summary>
        /// 所有人可见（0）
        /// </summary>
        All = 0,

        /// <summary>
        /// 仅好友可见（1）
        /// </summary>
        Friend = 1,

        /// <summary>
        /// 仅自己可见（2）
        /// </summary>
        Self = 2,

        /// <summary>
        /// 已删除状态（99）假删
        /// </summary>
        Delete = 99
    }
}
