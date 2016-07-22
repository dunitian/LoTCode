using PawChina.IDal;

namespace PawChina.BLL
{
    public class ChineseInfoBLL : BaseBLL<Model.ChineseInfo>, IBLL.IChineseInfoBLL
    {
        /// <summary>
        /// 实现父类抽象方法
        /// </summary>
        /// <returns></returns>
        protected override IBaseDal<Model.ChineseInfo> GetModelDal()
        {
            return Factory.DalFactory.Resolve<IChineseInfoDal>();
        }
    }
}
