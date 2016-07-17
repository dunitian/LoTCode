using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoTBlog.Back.Models
{
    /// <summary>
    /// 文章系列（查询专用）
    /// </summary>
    public partial class ArticleTemp
    {
        public int Id { get; set; }

        /// <summary>
        /// 标题（最多25个字）
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章分类
        /// </summary>
        public string ArticleType { get; set; }

        /// <summary>
        /// 排序（升序）0在最前面
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime { get; set; }        

        /// <summary>
        /// 状态（0,所有人可见，1,好友可见，2,仅自己可见,99删除）
        /// </summary>
        public LoT.Enums.ArticleStatusEnum Status { get; set; }
    }
}