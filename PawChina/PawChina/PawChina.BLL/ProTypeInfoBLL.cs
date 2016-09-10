using PawChina.IDal;
using PawChina.Model;
using System.Threading.Tasks;

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
        /// <summary>
        /// 按模型查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> QueryAsync(QueryModel model)
        {
            //todo:QueryAsync protype
            return null;
        }
    }
}
