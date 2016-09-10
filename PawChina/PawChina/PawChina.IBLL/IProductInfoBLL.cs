using System.Threading.Tasks;

namespace PawChina.IBLL
{
    public interface IProductInfoBLL : IBaseBLL<Model.ProductInfo>
    {
        /// <summary>
        /// 根据模型查询数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> QueryAsync(Model.QueryModel model);
    }
}
