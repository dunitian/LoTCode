using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoTBlog.Models
{
    public partial class ArticleTemp
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string TContent { get; set; }

        /// <summary>
        /// 作者（最多15个字）
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 默认展图
        /// </summary>
        public string DisplayPic { get; set; }

        /// <summary>
        /// 文章分类信息
        /// </summary>
        public IList<LoT.Model.ArticleType> ArticleTypeList { get; set; }
    }
}