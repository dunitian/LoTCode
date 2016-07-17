using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Service
{
    public partial class ArticleService : BaseService<Article>, IArticleService
    {
        /// <summary>
        /// 给父类指派一个ArticleDal对象
        /// </summary>
        /// <returns></returns>
        protected override IDal.IBaseDal<Article> GetModelDal()
        {
            return dbSession.ArticleDal;
        }

        /// <summary>
        /// 添加一篇文章
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="seoInfo">SEO</param>
        /// <returns></returns>
        public int AddArticle(Article article, SeoTKD seoInfo)
        {
            return dbSession.ArticleDal.AddArticle(article, seoInfo);
        }

        /// <summary>
        /// 修改一篇文章
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="seoInfo">SEO</param>
        /// <returns></returns>
        public int UpdateArticle(Article article, SeoTKD seoInfo)
        {
            return dbSession.ArticleDal.UpdateArticle(article, seoInfo);
        }
    }
}
