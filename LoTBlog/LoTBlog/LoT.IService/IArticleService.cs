using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.IService
{
    /// <summary>
    /// 文章表
    /// </summary>
    public interface IArticleService : IBaseService<Article>
    {
        /// <summary>
        /// 添加一篇文章
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="seoInfo">SEO</param>
        /// <returns></returns>
        int AddArticle(Article article, SeoTKD seoInfo);

        /// <summary>
        /// 修改一篇文章
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="seoInfo">SEO</param>
        /// <returns></returns>
        int UpdateArticle(Article article, SeoTKD seoInfo);
    }
}
