using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Service
{
    public partial class ArticleTypeService : BaseService<ArticleType>, IArticleTypeService
    {
        /// <summary>
        /// 给父类指派一个ArticleTypeDal对象
        /// </summary>
        /// <returns></returns>
        protected override IDal.IBaseDal<ArticleType> GetModelDal()
        {
            return dbSession.ArticleTypeDal;
        }
    }
}
