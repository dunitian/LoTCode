using System.Text;
using PawChina.IDal;
using PawChina.Model;
using System.Threading.Tasks;

namespace PawChina.BLL
{
    public class NoteDisPlayImgBLL : BaseBLL<Model.NoteDisPlayImg>, IBLL.INoteDisPlayImgBLL
    {
        /// <summary>
        /// 实现父类抽象方法
        /// </summary>
        /// <returns></returns>
        protected override IBaseDal<NoteDisPlayImg> GetModelDal()
        {
            return Factory.DalFactory.Resolve<INoteDisPlayImgDal>();
        }

        /// <summary>
        /// 根据模型查询数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <summary>
        /// 根据模型查询数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> QueryAsync(QueryModel model)
        {
            StringBuilder sqlWhere = new StringBuilder();
            StringBuilder sqlStr = new StringBuilder("select DId,DTitle,DPicUrl,DataStatus from NoteDisPlayImg");
            StringBuilder sqlCount = new StringBuilder("select count(1) from NoteDisPlayImg");
            dynamic pms1 = new System.Dynamic.ExpandoObject();
            dynamic pms2 = new System.Dynamic.ExpandoObject();

            #region 条件系列
            //展图状态
            if (model.DataStatus == StatusEnum.Init) { model.DataStatus = StatusEnum.Normal; }
            sqlWhere.Append(" where DataStatus=@DataStatus");
            pms1.DataStatus = (int)model.DataStatus;

            //展图标题
            if (!model.Title.IsNullOrWhiteSpace())
            {
                sqlWhere.Append(" and DTitle like @DTitle");
                pms1.DTitle = string.Format("%{0}%", model.Title);
            }
            #endregion

            pms2 = pms1;
            sqlStr.Append(sqlWhere.ToString());
            sqlCount.Append(sqlWhere.ToString());

            //排序，没有排序或者有SQLI嫌疑的就变为默认
            if (model.OrderStr.IsNullOrWhiteSpace() || model.OrderStr.Contains("undefined") || model.OrderStr.IsSQLI())
            {
                model.OrderStr = "DId desc";
            }
            pms2.OrderStr = model.OrderStr;

            #region 分页系列
            if (model.Offset == 0 && model.PageSize == 0)//不分页==》这时候两个条件是一样的
            {
                return await modelDal.PageLoadAsync(sqlStr.ToString(), pms1, sqlCount.ToString(), pms2);
            }
            if (model.Offset < 0) { model.Offset = 0; }
            if (model.PageSize < 1) { model.PageSize = 10; }
            model.PageIndex = model.Offset / model.PageSize + 1;

            pms1.PageIndex = model.PageIndex;
            pms1.PageSize = model.PageSize;

            sqlStr.Insert(0, string.Format("select * from(select row_number() over(order by {0}) Id,* from (", model.OrderStr));
            sqlStr.Append(") TempA) as TempInfo where Id<= @PageIndex * @PageSize and Id>(@PageIndex-1)*@PageSize");
            return await PageLoadAsync(sqlStr.ToString(), pms1, sqlCount.ToString(), pms2);
            #endregion
        }

    }
}
