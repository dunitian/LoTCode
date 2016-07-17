using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Service
{
    public partial class PeopleDisPhotoService : BaseService<PeopleDisPhoto>, IPeopleDisPhotoService
    {
        /// <summary>
        /// 给父类指派一个PeopleDisPhotoDal对象
        /// </summary>
        /// <returns></returns>
        protected override IDal.IBaseDal<PeopleDisPhoto> GetModelDal()
        {
            return dbSession.PeopleDisPhotoDal;
        }
    }
}
