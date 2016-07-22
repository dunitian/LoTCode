using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoTData.Core
{
    /// <summary>
    /// 后期可以手动添加MySql，Sqlite等
    /// </summary>
    public partial class ConnFactory
    {
        private static readonly string connString = ConfigHelper.GetConnectionString("SqlConnStr");
        private static SqlConnection conn;
        /// <summary>
        /// 获取Connection
        /// </summary>
        /// <returns></returns>
        public async static Task<SqlConnection> GetConnection()
        {
            if (conn == null)
            {
                conn = new SqlConnection(connString);
            }
            try
            {
                await conn.OpenAsync();
            }
            catch //(System.Exception ex)
            {
                throw new System.Exception("打开数据库出错~~~ConnFactory：conn.OpenAsync()");
            }
            return conn;
        }
    }
}