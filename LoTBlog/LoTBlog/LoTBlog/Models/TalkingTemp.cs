using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoTBlog.Models
{
    public class TalkingTemp
    {
        public int Id { get; set; }

        /// <summary>
        /// 标题（最多25个字）
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容（最多500个字）
        /// </summary>
        public string Say { get; set; }

        /// <summary>
        /// 展览图（默认图：前台在默认图库里面随机取一张）
        /// </summary>
        public string DisplayPic { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public int HitCount { get; set; }

    }
}