using System.Collections.Generic;

namespace PawChina.IDal
{
    /// <summary>
    /// BaseDal
    /// </summary>
    public partial interface IBaseDal
    {
        #region 基础方法

        #endregion

        #region 扩展方法
        T Get<T>(int id);
        T Get<T>(long id);
        T Get<T>(System.Guid id);
        IEnumerable<T> GetAll<T>();
        int Insert<T>(T obj);
        int Insert<T>(IEnumerable<T> list);
        bool Update<T>(T obj);
        bool Update<T>(IEnumerable<T> list); 
        #endregion
    }
}
