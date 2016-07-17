using LoT.IDal;
using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Service
{
    public partial class UserRegInfoService : BaseService<UserRegInfo>, IUserRegInfoService
    {
        #region dbSession的说明
        //如果把这个放在UserRegInfoService的构造函数中会导致dbSession为null，因为默认先调用父类构造方法，里面让你先实现GetModelDal()，所以程序会先执行 protected override IBaseDal<UserRegInfo> GetModelDal() 然后再执行构造函数，会导致dbSession没赋值前就调用了，也就报空引用异常了
        //dbSession = DbSessionFactory.GetDbSession();//获取DbSession
        //再优化，直接放父类里面，子类直接调用，就不用每个子类里面再加了 by 逆天
        #endregion

        /// <summary>
        /// 给BaseDal一个Dal实例对象
        /// </summary>
        /// <returns></returns>
        protected override IBaseDal<UserRegInfo> GetModelDal()
        {
            return dbSession.UserRegInfoDal;//通过DbSession获取Dal实例,并指派给父类
        }

    }
}
