using System;
using System.Text;
using PawChina.IDal;
using PawChina.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        public Task<IEnumerable<NoteInfo>> QueryAsync(QueryModel model)
        {
            var list= modelDal.GetAllAsync();
            return list;
            //StringBuilder sqlWhere = new StringBuilder();
            //var p = DapperDataAsync.GetDynamicParameters();
            //var p2 = DapperDataAsync.GetDynamicParameters();
            //StringBuilder sqlStr = new StringBuilder("select NId,NTitle,NAuthor,NHitCount,NPush,NDataStatus,NCreateTime,NUpdateTime from NoteInfo");
            //StringBuilder sqlCount = new StringBuilder("select count(1) from NoteInfo");

            //#region 条件系列
            ////文章状态
            //if (model.DataStatus == StatusEnum.Init) { model.DataStatus = StatusEnum.Normal; }
            //p.Add("NDataStatus", (int)model.DataStatus);
            //sqlWhere.Append(" where NDataStatus=@NDataStatus");
            ////文章标题
            //if (!model.Title.IsNullOrWhiteSpace())
            //{
            //    p.Add("NTitle", string.Format("%{0}%", model.Title));
            //    sqlWhere.Append(" and NTitle like @NTitle");
            //}
            ////创建时间
            //if (DateTime.Compare(model.StartTime, DateTime.MinValue) > 0)
            //{
            //    p.Add("StartTime", model.StartTime);
            //    sqlWhere.Append(string.Format(" and NCreateTime>=@StartTime", model.StartTime));
            //}
            ////更新时间
            //if (DateTime.Compare(model.EndTime, DateTime.MinValue) > 0 && DateTime.Compare(model.EndTime, DateTime.Now) <= 0)
            //{
            //    p.Add("EndTime", model.EndTime);
            //    sqlWhere.Append(string.Format(" and NCreateTime<=@EndTime", model.EndTime));
            //}
            //#endregion

            //sqlStr.Append(sqlWhere.ToString());
            //sqlCount.Append(sqlWhere.ToString());
            //p2.AddDynamicParams(p);

            //#region 分页系列
            //if (model.Offset == 0 && model.PageSize == 0)//不分页
            //{
            //    return await modelDal.PageLoadAsync(sqlStr.ToString(), p, sqlCount.ToString(), p2);
            //}
            //if (model.Offset < 0) { model.Offset = 0; }
            //if (model.PageSize < 1) { model.PageSize = 10; }
            //model.PageIndex = model.Offset / model.PageSize + 1;

            //p.Add("PageIndex", model.PageIndex);
            //p.Add("PageSize", model.PageSize);
            //sqlStr.Insert(0, "select * from(select row_number() over(order by NCreateTime) Id,* from (");
            //sqlStr.Append(") TempA) as TempInfo where Id<= @PageIndex * @PageSize and Id>(@PageIndex-1)*@PageSize");
            //return await PageLoadAsync(sqlStr.ToString(), p, sqlCount.ToString(), p2);
            //#endregion
            return null;
        }

    }
}
