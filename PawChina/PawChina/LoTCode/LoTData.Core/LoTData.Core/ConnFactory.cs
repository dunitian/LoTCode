using System.Data.SqlClient;

namespace LoTData.Core
{
    /// <summary>
    /// 后期可以手动添加MySql，Sqlite等
    /// </summary>
    public partial class ConnFactory
    {
        private static readonly string connString = ConfigHelper.GetConnectionString("SqlConnStr");
        /// <summary>
        /// 获取Connection
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connString);
        }
    }
}