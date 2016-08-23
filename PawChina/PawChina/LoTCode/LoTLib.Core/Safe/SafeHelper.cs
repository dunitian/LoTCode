using System;
using System.Text.RegularExpressions;

public static partial class SafeHelper
{
    #region 常见加密
    /// <summary>
    /// SHA-1加密
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string GetShaOne(string input)
    {
        var shaOne = System.Security.Cryptography.SHA1.Create();
        byte[] shaOneBuffer = shaOne.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
        shaOne.Clear();
        return BitConverter.ToString(shaOneBuffer).Replace("-", "").ToLower();//另一种简单的方法
    }
    #endregion


    #region 判断字符串中是否有SQL攻击代码
    /// <summary>
    /// 判断字符串中是否有SQL攻击代码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsSQLI(this string input)
    {
        string SqlStr = @"and|or|exec|execute|insert|select|delete|update|alter|create|drop|count|\*|chr|char|--|mid|substring|master|truncate|declare|xp_cmdshell|restore|backup|net +user|net +localgroup +administrators";
        try
        {
            return Regex.IsMatch(input, SqlStr, RegexOptions.IgnoreCase);
        }
        catch
        {
            return false;
        }
    } 
    #endregion

    #region 获取HTML里面的中文
    public static string GetChinese(string Htmlstring)
    {
        //删除脚本   
        Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);

        //删除HTML   
        Regex regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
        Htmlstring = regex.Replace(Htmlstring, "");
        Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&quot;", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&amp;", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&lt;", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&gt;", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&nbsp;", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&iexcl;", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&cent;", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&pound;", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&copy;", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

        return Htmlstring;
        //去除其他特殊符号
        //Htmlstring = Regex.Replace(Htmlstring, @"[--~-.<>!@#$%^&*()_+=*/\\！￥……（）“”‘’：/》《~:;'""]+", "", RegexOptions.IgnoreCase);
    }
    #endregion
}
