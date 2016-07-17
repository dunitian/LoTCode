using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Service
{
    public partial class ArticleTagService : BaseService<ArticleTag>, IArticleTagService
    {
        /// <summary>
        /// 给父类指派一个ArticleTagDal对象
        /// </summary>
        /// <returns></returns>
        protected override IDal.IBaseDal<ArticleTag> GetModelDal()
        {
            return dbSession.ArticleTagDal;
        }
    }
}
