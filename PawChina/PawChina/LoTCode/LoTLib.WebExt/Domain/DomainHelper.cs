using System;

public partial class DomainHelper
{
    /// <summary>
    /// 验证是否是其他域名（ture则非本域名）
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static bool IsOtherDomain(System.Web.HttpRequestBase request)
    {
        var urlReferrer = request.UrlReferrer;//注意一下可能为空
        //非本域名
        if (urlReferrer != null && Uri.Compare(urlReferrer, request.Url, UriComponents.HostAndPort, UriFormat.SafeUnescaped, StringComparison.CurrentCulture) != 0)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 判断URL是否是本域名（防劫持）
    /// </summary>
    /// <param name="request"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public static bool IsOtherDomain(System.Web.HttpRequestBase request,Uri url)
    {
        //非本域名
        if (Uri.Compare(url, request.Url, UriComponents.HostAndPort, UriFormat.SafeUnescaped, StringComparison.CurrentCulture) != 0)
        {
            return true;
        }
        return false;
    }
}