using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Service
{
    public partial class QQModelService : BaseService<QQModel>, IQQModelService
    {
        /// <summary>
        /// 给父类指派一个QQModelDal对象
        /// </summary>
        /// <returns></returns>
        protected override IDal.IBaseDal<QQModel> GetModelDal()
        {
            return dbSession.QQModelDal;
        }
    }
}
