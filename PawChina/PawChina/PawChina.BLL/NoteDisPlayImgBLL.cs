using System.Threading.Tasks;
using PawChina.IDal;
using PawChina.Model;

namespace PawChina.BLL
{
    public class NoteDisPlayImgBLL : BaseBLL<Model.NoteDisPlayImg>, IBLL.INoteDisPlayImgBLL
    {
        /// <summary>
        /// 根据模型查询数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> QueryAsync(QueryModel model)
        {
            return null;
        }

        /// <summary>
        /// 实现父类抽象方法
        /// </summary>
        /// <returns></returns>
        protected override IBaseDal<Model.NoteDisPlayImg> GetModelDal()
        {
            return Factory.DalFactory.Resolve<INoteDisPlayImgDal>();
        }
    }
}
