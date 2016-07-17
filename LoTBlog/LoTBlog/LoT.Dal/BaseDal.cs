using LoT.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LoT.Dal
{
    //T必须是引用类型（Set<T>）
    public partial class BaseDal<T> where T : class
    {
        DbContext dbContext = EFContextFactory.GetEFContext();

        #region 增加一个实体
        /// <summary>
        /// 增加一个实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddModel(T model)
        {
            dbContext.Set<T>().Add(model);
            return dbContext.SaveChanges();
        }
        #endregion

        #region 删除指定ID的Model~反射实现
        /// <summary>
        /// 删除指定ID的Model~不推荐使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteModel(int id)
        {
            Type t = typeof(T);//获取 要修改对象 的类型属性
            PropertyInfo pi = t.GetProperty("Status");//获取属性对象

            T model = dbContext.Set<T>().Find(id);
            pi.SetValue(model, StatusEnum.Delete, null);//model.Status = 99;
            dbContext.Entry(model).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        #endregion

        #region 删除指定IDs的Models~反射实现
        /// <summary>
        /// 删除指定IDs的Models~不推荐使用
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteModels(int[] ids)
        {
            Type t = typeof(T);//获取 要修改对象 的类型属性
            PropertyInfo pi = t.GetProperty("Status");//获取属性对象

            foreach (var item in ids)
            {
                T model = dbContext.Set<T>().Find(item);
                pi.SetValue(model, StatusEnum.Delete, null);//model.Status = 99;
                dbContext.Entry(model).State = EntityState.Modified;
            }
            return dbContext.SaveChanges();
        }
        #endregion

        #region 修改指定Model
        /// <summary>
        /// 修改指定Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateModel(T model)
        {
            dbContext.Entry(model).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        #endregion

        #region 根据自定义条件修改指定Model
        /// <summary>
        /// 修改指定Model
        /// </summary>
        /// <param name="whereLambda">约束实体的条件</param>
        /// <param name="updateAcion">需要修改什么</param>
        /// <returns></returns>
        public int UpdateModel(Expression<Func<T, bool>> whereLambda, Action<T> updateAcion)
        {
            var modelList = dbContext.Set<T>().Where(whereLambda);
            foreach (var model in modelList)
            {
                updateAcion(model);//更新实体
                dbContext.Entry(model).State = EntityState.Modified;
            }
            return dbContext.SaveChanges();
        }
        #endregion

        #region 根据ID查指定Model
        /// <summary>
        /// 根据ID查指定Model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T FindModel(int id)
        {
            return dbContext.Set<T>().Find(id);
        }
        #endregion

        #region 根据指定条件查询
        /// <summary>
        /// 根据指定条件查询
        /// </summary>
        /// <param name="whereLambada"></param>
        /// <returns></returns>
        public IQueryable<T> PageLoad(Expression<Func<T, bool>> whereLambada)
        {
            return dbContext.Set<T>().Where(whereLambada);
        }
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
        public IQueryable<T> PageLoad(Expression<Func<T, bool>> whereLambada, Expression<Func<T, object>> orderLambada, bool desc, int pageIndex, int pageSize, out int total)
        {
            IQueryable<T> temp = dbContext.Set<T>().Where(whereLambada);
            total = temp.Count();
            if (desc)
            {
                return temp.OrderByDescending(orderLambada).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return temp.OrderBy(orderLambada).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }
        #endregion
    }
}
