using LoT.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace LoT.Dal
{
    /// <summary>
    /// 上下文工厂~如果放Facroty类库里面就会出现Dal和Factory重复引用的现象
    /// </summary>
    public partial class EFContextFactory
    {
        /// <summary>
        /// 私有化构造函数
        /// </summary>
        private EFContextFactory()
        {

        }

        /// <summary>
        /// 获取唯一的上下文
        /// </summary>
        /// <returns></returns>
        public static DbContext GetEFContext()
        {
            //数据槽是线程内独占的一个集合 ~ 保证一次请求中上下文实例唯一
            DbContext dbContext = CallContext.GetData("EFContext") as DbContext;
            if (dbContext == null)
            {
                dbContext = new EFContext();//Model层的EFContext
                CallContext.SetData("EFContext", dbContext);
            }
            return dbContext;
        }
    }
}
