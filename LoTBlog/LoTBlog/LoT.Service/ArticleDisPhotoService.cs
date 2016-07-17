using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Service
{
    public partial class ArticleDisPhotoService : BaseService<ArticleDisPhoto>, IArticleDisPhotoService
    {
        /// <summary>
        /// 给父类指派一个ArticleDisPhotoDal对象
        /// </summary>
        /// <returns></returns>
        protected override IDal.IBaseDal<ArticleDisPhoto> GetModelDal()
        {
            return dbSession.ArticleDisPhotoDal;
        }
    }
}
