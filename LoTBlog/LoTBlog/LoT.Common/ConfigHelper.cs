using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace LoT.Common
{
    public static partial class ConfigHelper
    {
        public static string GetValueForConfigAppKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
