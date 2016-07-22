using System.Data;
using PawChina.IBLL;
using PawChina.IDal;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PawChina.BLL
{
    public abstract partial class BaseBLL<T> : IBaseBLL<T> where T : class, new()
    {
        #region 获取Dal对象
        protected IBaseDal<T> modelDal;
        protected abstract IBaseDal<T> GetModelDal();//abstract~子类继承后必须实现
        public BaseBLL()
        {
            modelDal = GetModelDal();
        }
        #endregion

        #region 基础方法

        #region 查询系列
        /// <summary>
        /// 单个值返回值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return await modelDal.ExecuteScalarAsync(sql, param, transaction, commandTimeout, commandType);
        }
        /// <summary>
        /// 强类型查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return await modelDal.QueryAsync(sql, param, transaction, commandTimeout, commandType);
        }
        /// <summary>
        /// 动态类型查询 | 多映射动态查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public async Task<IEnumerable<dynamic>> QueryAsyncDynamic(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return await modelDal.QueryAsync(sql, param, transaction, commandTimeout, commandType);
        }
        #endregion

        #region 增删改系
        /// <summary>
        /// 增删改系
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            return await modelDal.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }
        #endregion

        #endregion

        #region 扩展方法

        #region 查询系
        /// <summary>
        /// 获取Model-Key为int类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(int id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await modelDal.GetAsync(id, transaction, commandTimeout);
        }
        /// <summary>
        /// 获取Model-Key为long类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(long id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await modelDal.GetAsync(id, transaction, commandTimeout);
        }
        /// <summary>
        /// 获取Model-Key为Guid类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(System.Guid id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await modelDal.GetAsync(id, transaction, commandTimeout);
        }
        /// <summary>
        /// 获取Model集合（没有Where条件）
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await modelDal.GetAllAsync();
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 插入一个Model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await modelDal.InsertAsync(model, transaction, commandTimeout);
        }

        /// <summary>
        /// 更新一个Model
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entityToUpdate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async Task<T> UpdateAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await modelDal.UpdateAsync(model, transaction, commandTimeout);
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询(为什么不用out，请参考：http://www.cnblogs.com/dunitian/p/5556909.html)
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="p">动态参数</param>
        /// <param name="sqlTotal">total语句</param>
        /// <param name="p2">Total动态参数</param>
        /// <returns></returns>
        public async Task<string> PageLoadAsync(string sql, object p = null, string sqlTotal = "", object p2 = null)
        {
            return await modelDal.PageLoadAsync(sql, p, sqlTotal, p2);
        }
        #endregion

        #endregion
    }
}
