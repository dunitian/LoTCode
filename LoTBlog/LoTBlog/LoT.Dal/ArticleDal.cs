using LoT.IDal;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LoT.Dal
{
    public partial class ArticleDal : BaseDal<Article>, IArticleDal
    {
        DbContext dbContext = EFContextFactory.GetEFContext();//拿到dbContext

        /// <summary>
        /// 添加一篇文章
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="seoInfo">SEO</param>
        /// <returns></returns>
        public int AddArticle(Article article, SeoTKD seoInfo)
        {
            //添加SeoTKD信息
            SeoTKD seoInfoTemp = dbContext.Set<SeoTKD>().Add(seoInfo);

            //把需要的信息放到Article表中
            article.SeoId = seoInfoTemp.Id;
            article.SeoInfo = seoInfoTemp;

            //保存Article
            dbContext.Set<Article>().Add(article);

            //批量提交
            return dbContext.SaveChanges();
        }


        /// <summary>
        /// 修改一篇文章
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="seoInfo">SEO</param>
        /// <returns></returns>
        public int UpdateArticle(Article article, SeoTKD seoInfo)
        {
            //修改SeoTKD信息
            dbContext.Entry(seoInfo).State = EntityState.Modified;

            //修改Article信息
            dbContext.Entry(article).State = EntityState.Modified;

            //批量提交
            return dbContext.SaveChanges();
        }
    }
}
