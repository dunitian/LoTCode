using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PawChina.IBLL
{
    public interface IBaseBLL<T>
    {
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
        Task<T> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
        /// <summary>
        /// 强类型查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
        /// <summary>
        /// 动态类型查询 | 多映射动态查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> QueryAsyncDynamic(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
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
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
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
        Task<T> GetAsync(int id, IDbTransaction transaction = null, int? commandTimeout = null);
        /// <summary>
        /// 获取Model-Key为long类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<T> GetAsync(long id, IDbTransaction transaction = null, int? commandTimeout = null);
        /// <summary>
        /// 获取Model-Key为Guid类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<T> GetAsync(System.Guid id, IDbTransaction transaction = null, int? commandTimeout = null);
        /// <summary>
        /// 获取Model集合（没有Where条件）
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();
        #endregion

        #region 增删改
        /// <summary>
        /// 插入一个Model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<int> InsertAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 更新一个Model
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entityToUpdate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null);
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
        Task<string> PageLoadAsync(string sql, object p = null, string sqlTotal = "", object p2 = null);
        #endregion

        #endregion
    }
}
