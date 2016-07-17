using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LoT.IDal
{
    public interface IBaseDal<T> where T : class,new()
    {
        #region 增加一个实体
        /// <summary>
        /// 增加一个实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddModel(T model);
        #endregion

        #region 删除指定ID的Model~反射实现
        /// <summary>
        /// 删除指定ID的Model~不推荐使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteModel(int id);
        #endregion

        #region 删除指定IDs的Models~反射实现
        /// <summary>
        /// 删除指定IDs的Models~不推荐使用
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        int DeleteModels(int[] ids);
        #endregion

        #region 修改指定Model
        /// <summary>
        /// 修改指定Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int UpdateModel(T model);
        #endregion

        #region 根据自定义条件修改指定Model
        /// <summary>
        /// 修改指定Model
        /// </summary>
        /// <param name="whereLambda">约束实体的条件</param>
        /// <param name="updateAcion">需要修改什么</param>
        /// <returns></returns>
        int UpdateModel(Expression<Func<T, bool>> whereLambda, Action<T> updateAcion);
        #endregion

        #region 根据ID查指定Model
        /// <summary>
        /// 根据ID查指定Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindModel(int id);
        #endregion

        #region 根据指定条件查询
        /// <summary>
        /// 根据指定条件查询
        /// </summary>
        /// <param name="whereLambada"></param>
        /// <returns></returns>
        IQueryable<T> PageLoad(Expression<Func<T, bool>> whereLambada);
        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereLambada">Where的lambada表达式</param>
        /// <param name="orderLambada">orderBy的lambada表达式-TKey在此赋值</param>
        /// <param name="desc">是否是降序排列</param>
        /// <param name="pageIndex">当前页数（从1开始）</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="total">总共条数</param>
        /// <returns></returns>
        IQueryable<T> PageLoad(Expression<Func<T, bool>> whereLambada, Expression<Func<T, object>> orderLambada, bool desc, int pageIndex, int pageSize, out int total);
        #endregion
    }
}
