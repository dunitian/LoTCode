using System;
using System.Text;
using PawChina.IDal;
using PawChina.Model;
using System.Threading.Tasks;

namespace PawChina.BLL
{
    public class NoteInfoBLL : BaseBLL<NoteInfo>, IBLL.INoteInfoBLL
    {
        /// <summary>
        /// 实现父类抽象方法
        /// </summary>
        /// <returns></returns>
        protected override IBaseDal<NoteInfo> GetModelDal()
        {
            return Factory.DalFactory.Resolve<INoteInfoDal>();
        }

        /// <summary>
        /// 根据模型查询数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> QueryAsync(QueryModel model)
        {
            StringBuilder sqlWhere = new StringBuilder();
            StringBuilder sqlStr = new StringBuilder("select NId,NTitle,NAuthor,NHitCount,NPush,NDataStatus,NCreateTime,NUpdateTime from NoteInfo");
            StringBuilder sqlCount = new StringBuilder("select count(1) from NoteInfo");
            dynamic pms1 = new System.Dynamic.ExpandoObject();
            dynamic pms2 = new System.Dynamic.ExpandoObject();

            #region 条件系列
            //文章状态
            if (model.DataStatus == StatusEnum.Init) { model.DataStatus = StatusEnum.Normal; }
            sqlWhere.Append(" where NDataStatus=@NDataStatus");
            pms1.NDataStatus = (int)model.DataStatus;
            pms2.NDataStatus = (int)model.DataStatus;

            //文章标题
            if (!model.Title.IsNullOrWhiteSpace())
            {
                sqlWhere.Append(" and NTitle like @NTitle");
                pms1.NTitle = string.Format("%{0}%", model.Title);
                pms2.NTitle = string.Format("%{0}%", model.Title);
            }
            //创建时间
            if (DateTime.Compare(model.StartTime, DateTime.MinValue) > 0)
            {
                sqlWhere.Append(string.Format(" and NCreateTime>=@StartTime", model.StartTime));
                pms1.StartTime = model.StartTime;
                pms2.StartTime = model.StartTime;
            }
            //更新时间
            if (DateTime.Compare(model.EndTime, DateTime.MinValue) > 0 && DateTime.Compare(model.EndTime, DateTime.Now) <= 0)
            {
                sqlWhere.Append(string.Format(" and NCreateTime<=@EndTime", model.EndTime));
                pms1.EndTime = model.EndTime;
                pms2.EndTime = model.EndTime;
            }
            #endregion

            sqlStr.Append(sqlWhere.ToString());
            sqlCount.Append(sqlWhere.ToString());

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

            sqlStr.Insert(0, "select * from(select row_number() over(order by NCreateTime) Id,* from (");
            sqlStr.Append(") TempA) as TempInfo where Id<= @PageIndex * @PageSize and Id>(@PageIndex-1)*@PageSize");
            return await PageLoadAsync(sqlStr.ToString(), pms1, sqlCount.ToString(), pms2);
            #endregion
        }

    }
}
