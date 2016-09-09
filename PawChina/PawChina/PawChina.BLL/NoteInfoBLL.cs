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
            dynamic pms1 = new System.Dynamic.ExpandoObject();
            dynamic pms2 = new System.Dynamic.ExpandoObject();

            #region 条件系列
            //文章状态
            if (model.DataStatus == StatusEnum.Init) { model.DataStatus = StatusEnum.Normal; }
            sqlWhere.Append(" where NDataStatus=@NDataStatus");
            pms1.NDataStatus = (int)model.DataStatus;

            //文章标题
            if (!model.Title.IsNullOrWhiteSpace())
            {
                sqlWhere.Append(" and NTitle like @NTitle");
                pms1.NTitle = string.Format("%{0}%", model.Title);
            }
            //创建时间
            if (model.StartTime > 0)
            {
                sqlWhere.Append(string.Format(" and NCreateTime>=@StartTime", model.StartTime));
                pms1.StartTime = model.StartTime;
            }
            //更新时间
            if (model.EndTime > model.StartTime)
            {
                sqlWhere.Append(string.Format(" and NCreateTime<=@EndTime", model.EndTime));
                pms1.EndTime = model.EndTime;
            }
            #endregion

            pms2 = pms1;

            //排序，没有排序或者有SQLI嫌疑的就变为默认
            if (model.OrderStr.IsNullOrWhiteSpace() || model.OrderStr.Contains("undefined") || model.OrderStr.IsSQLI())
            {
                model.OrderStr = "NCreateTime desc";
            }
            pms2.OrderStr = model.OrderStr;

            return await PageLoadAsync(model, "NoteInfo", sqlWhere.ToString(), pms1, pms2);
        }
    }
}
