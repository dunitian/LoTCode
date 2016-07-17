using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Service
{
    public partial class BaseInfoService : BaseService<BaseInfo>, IBaseInfoService
    {
        protected override IDal.IBaseDal<BaseInfo> GetModelDal()
        {
            return dbSession.BaseInfoDal;
        }
    }
}
