using PawChina.IDal;

namespace PawChina.BLL
{
    public class SeoTKDBLL : BaseBLL<Model.SeoTKD>, IBLL.ISeoTKDBLL
    {
        /// <summary>
        /// 实现父类抽象方法
        /// </summary>
        /// <returns></returns>
        protected override IBaseDal<Model.SeoTKD> GetModelDal()
        {
            return Factory.DalFactory.Resolve<ISeoTKDDal>();
        }
    }
}
