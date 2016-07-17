using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Service
{
    public partial class TalkingService : BaseService<Talking>, ITalkingService
    {
        /// <summary>
        /// 给父类指派一个TalkingDal对象
        /// </summary>
        /// <returns></returns>
        protected override IDal.IBaseDal<Talking> GetModelDal()
        {
            return dbSession.TalkingDal;
        }
    }
}
