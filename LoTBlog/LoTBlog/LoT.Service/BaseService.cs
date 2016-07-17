using LoT.Factory;
using LoT.IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LoT.Service
{
    public abstract partial class BaseService<T> where T : class,new()
    {
        protected IDbSession dbSession = DbSessionFactory.GetDbSession();//获取DbSession，通过DbSession可以让业务层和数据层解耦

        #region 获取Dal对象
        protected IBaseDal<T> modelDal;
        protected abstract IBaseDal<T> GetModelDal();//abstract~子类继承后必须实现

        public BaseService()//在实例化的时候会自动为modelDal赋值
        {
            modelDal = GetModelDal();
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 增加一个实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        public bool AddModel(T model)
        {
            return modelDal.AddModel(model) > 0;
        }

        /// <summary>
        /// 删除指定ID的Model~不推荐使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns>是否成功</returns>
        public bool DeleteModel(int id)
        {
            return modelDal.DeleteModel(id) > 0;
        }

        /// <summary>
        /// 删除指定IDs的Models~不推荐使用
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>成功条数</returns>
        public int DeleteModels(int[] ids)
        {
            return modelDal.DeleteModels(ids);
        }

        /// <summary>
        /// 修改指定Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns>是否成功</returns>
        public bool UpdateModel(T model)
        {
            return modelDal.UpdateModel(model) > 0;
        }

        /// <summary>
        /// 根据自定义条件修改指定Model
        /// </summary>
        /// <param name="whereLambda">约束实体的条件</param>
        /// <param name="updateAcion">需要修改什么</param>
        /// <returns>返回删除成几个</returns>
        public int UpdateModel(Expression<Func<T, bool>> whereLambda, Action<T> updateAcion)
        {
            return modelDal.UpdateModel(whereLambda, updateAcion);
        }

        /// <summary>
        /// 根据ID查指定Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Model</returns>
        public T FindModel(int id)
        {
            return modelDal.FindModel(id);
        }

        /// <summary>
        /// 根据指定条件查询
        /// </summary>
        /// <param name="whereLambada"></param>
        /// <returns></returns>
        public IQueryable<T> PageLoad(Expression<Func<T, bool>> whereLambada)
        {
            return modelDal.PageLoad(whereLambada);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey">Key-通过该列进行OrderBy倒序</typeparam>
        /// <param name="whereLambada">Where的lambada表达式</param>
        /// <param name="orderLambada">orderBy的lambada表达式</param>
        /// <param name="desc">是否是降序排列</param>
        /// <param name="pageIndex">当前页数（从1开始）</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="total">总共条数</param>
        /// <returns>IQueryable</returns>
        public IQueryable<T> PageLoad(Expression<Func<T, bool>> whereLambada, Expression<Func<T, object>> orderLambada, bool desc, int pageIndex, int pageSize, out int total)
        {
            return modelDal.PageLoad(whereLambada, orderLambada, desc, pageIndex, pageSize, out total);
        }
        #endregion
    }
}
