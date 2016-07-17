using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.Service
{
    public partial class ImgFlashService : BaseService<ImgFlash>, IImgFlashService
    {
        /// <summary>
        /// 给父类指派一个ImgFlashDal对象
        /// </summary>
        /// <returns></returns>
        protected override IDal.IBaseDal<ImgFlash> GetModelDal()
        {
            return dbSession.ImgFlashDal;
        }
    }
}
