using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.LogSystem
{
    /// <summary>
    /// 前期先记录日记~后期根据一些特征码自动进行一些自动化处理
    /// </summary>
    public static partial class LogHelper
    {
        /// <summary>
        /// 记录日记
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteLog(string msg)
        {
            ILog log = log4net.LogManager.GetLogger("log");
            log.Error(msg);
        }
    }
}
