using System.Configuration;

public partial class ConfigHelper
{
    /// <summary>
    /// 获取AppSetting
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetAppSetting(string key)
    {
        return ConfigurationManager.AppSettings[key];
    }

    /// <summary>
    /// 获取ConnectionString
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetConnectionString(string key)
    {
        return ConfigurationManager.ConnectionStrings[key].ConnectionString;
    }
}
