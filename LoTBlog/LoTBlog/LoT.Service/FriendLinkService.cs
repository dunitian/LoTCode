using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Service
{
    public partial class FriendLinkService : BaseService<FriendLink>, IFriendLinkService
    {
        /// <summary>
        /// 给父类指派一个FriendLinkDal对象
        /// </summary>
        /// <returns></returns>
        protected override IDal.IBaseDal<FriendLink> GetModelDal()
        {
            return dbSession.FriendLinkDal;
        }
    }
}
