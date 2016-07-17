using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Service
{
    public partial class SeoTKDService : BaseService<SeoTKD>, ISeoTKDService
    {
        /// <summary>
        /// 给父类指派一个SeoTKDService对象
        /// </summary>
        /// <returns></returns>
        protected override IDal.IBaseDal<SeoTKD> GetModelDal()
        {
            return dbSession.SeoTKDDal;
        }
    }
}
