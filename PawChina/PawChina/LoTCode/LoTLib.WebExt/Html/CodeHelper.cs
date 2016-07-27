using System.Web;

public static partial class CodeHelper
{
    /// <summary>
    /// Url编码
    /// </summary>
    /// <param name="objStr"></param>
    /// <returns></returns>
    public static string ToUrlEncode(this string objStr)
    {
        return HttpUtility.UrlEncode(objStr);
    }
    /// <summary>
    /// Url解码
    /// </summary>
    /// <param name="objStr"></param>
    /// <returns></returns>
    public static string ToUrlDecode(this string objStr)
    {
        return HttpUtility.UrlDecode(objStr);
    }
    /// <summary>
    /// HTML编码
    /// </summary>
    /// <param name="objStr"></param>
    /// <returns></returns>
    public static string ToHtmlEncode(this string objStr)
    {
        return HttpUtility.HtmlEncode(objStr);
    }
    /// <summary>
    /// HTML解码
    /// </summary>
    /// <param name="objStr"></param>
    /// <returns></returns>
    public static string ToHtmlDecode(this string objStr)
    {
        return HttpUtility.HtmlDecode(objStr);
    }
}