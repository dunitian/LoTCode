using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoTBlog.Back.Models
{
    public class TalkingTemp
    {
        public int Id { get; set; }

        /// <summary>
        /// 内容（最多500个字）
        /// </summary>
        public string Say { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 更新时间（第一次和创建时间是一样的）
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public int HitCount { get; set; }

        /// <summary>
        /// 状态（默认为0 0,所有人可见，1,好友可见，2,仅自己可见,99删除）
        /// </summary>
        public LoT.Enums.ArticleStatusEnum Status { get; set; }
    }
}