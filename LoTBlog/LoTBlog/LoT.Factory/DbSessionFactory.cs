using LoT.IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace LoT.Factory
{
    public partial class DbSessionFactory
    {
        /// <summary>
        /// 保证在一次请求中，使用的是同一个数据会话对象
        /// </summary>
        /// <returns></returns>
        public static IDbSession GetDbSession()
        {
            IDbSession dbSession = CallContext.GetData("DbSession") as IDbSession;
            if (dbSession == null)
            {
                dbSession = new DbSession();
                CallContext.SetData("DbSession", dbSession);
            }
            return dbSession;
        }
    }
}
