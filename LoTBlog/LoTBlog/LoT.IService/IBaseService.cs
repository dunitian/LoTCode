using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LoT.IService
{
    public interface IBaseService<T> where T : class,new()
    {
        /// <summary>
        /// 增加一个实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        bool AddModel(T model);

        /// <summary>
        /// 删除指定ID的Model~不推荐使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns>是否成功</returns>
        bool DeleteModel(int id);

        /// <summary>
        /// 删除指定IDs的Models~不推荐使用
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>成功条数</returns>
        int DeleteModels(int[] ids);

        /// <summary>
        /// 修改指定Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        bool UpdateModel(T model);

        /// <summary>
        /// 根据自定义条件修改指定Model
        /// </summary>
        /// <param name="whereLambda">约束实体的条件</param>
        /// <param name="updateAcion">需要修改什么</param>
        /// <returns></returns>
        int UpdateModel(Expression<Func<T, bool>> whereLambda, Action<T> updateAcion);

        /// <summary>
        /// 根据ID查指定Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Model</returns>
        T FindModel(int id);

        /// <summary>
        /// 根据指定条件查询
        /// </summary>
        /// <param name="whereLambada"></param>
        /// <returns></returns>
        IQueryable<T> PageLoad(Expression<Func<T, bool>> whereLambada);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereLambada">Where的lambada表达式</param>
        /// <param name="orderLambada">orderBy的lambada表达式</param>
        /// <param name="desc">是否是降序排列</param>
        /// <param name="pageIndex">当前页数（从1开始）</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="total">总共条数</param>
        /// <returns>IQueryable</returns>
        IQueryable<T> PageLoad(Expression<Func<T, bool>> whereLambada, Expression<Func<T, object>> orderLambada, bool desc, int pageIndex, int pageSize, out int total);
    }
}
