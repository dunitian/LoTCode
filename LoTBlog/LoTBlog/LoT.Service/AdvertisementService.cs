using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Service
{
    public partial class AdvertisementService : BaseService<Advertisement>, IAdvertisementService
    {
        /// <summary>
        /// 给父类指派一个AdvertisementDal对象
        /// </summary>
        /// <returns></returns>
        protected override IDal.IBaseDal<Advertisement> GetModelDal()
        {
            return dbSession.AdvertisementDal;
        }
    }
}
