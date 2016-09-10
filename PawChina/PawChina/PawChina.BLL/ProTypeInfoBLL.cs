using PawChina.IDal;

namespace PawChina.BLL
{
    public class ProTypeInfoBLL : BaseBLL<Model.ProTypeInfo>, IBLL.IProTypeInfoBLL
    {
        /// <summary>
        /// 实现父类抽象方法
        /// </summary>
        /// <returns></returns>
        protected override IBaseDal<Model.ProTypeInfo> GetModelDal()
        {
            return Factory.DalFactory.Resolve<IProTypeInfoDal>();
        }
    }
}
