using LoT.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LoT.Safe
{
    public partial class HtmlSafeHelper
    {
        #region 清除HTML标记
        ///<summary>   
        ///清除HTML标记   
        ///</summary>   
        ///<param name="NoHTML">包括HTML的源码</param>   
        ///<returns>已经去除后的文字</returns>   
        public static string NoHTML(string Htmlstring)
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

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");

            return Htmlstring;
        }
        #endregion

        #region 过滤HTML标签
        /// <summary>
        /// 过滤HTML标签
        /// </summary>
        /// <param name="html">HTML文本内容字符串</param>
        /// <returns>返回过滤标签后的内容</returns>
        public string RemoveTag(string html)
        {
            Regex regex1 = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex(@" href *= *[\s\S]*script *:", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@" no[\s\S]*=", RegexOptions.IgnoreCase);
            Regex regex4 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
            Regex regex5 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
            Regex regex6 = new Regex(@"\<img[^\>]+\>", RegexOptions.IgnoreCase);
            Regex regex7 = new Regex(@"</p>", RegexOptions.IgnoreCase);
            Regex regex8 = new Regex(@"<p>", RegexOptions.IgnoreCase);
            Regex regex9 = new Regex(@"<[^>]*>", RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记 
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性 
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
            html = regex4.Replace(html, ""); //过滤iframe 
            html = regex5.Replace(html, ""); //过滤frameset 
            html = regex6.Replace(html, ""); //过滤frameset 
            html = regex7.Replace(html, ""); //过滤frameset 
            html = regex8.Replace(html, ""); //过滤frameset 
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            html = html.Replace("&nbsp;", string.Empty);
            html = Regex.Replace(html, "[\f\n\r\t\v]", "");  //过滤回车换行制表符 
            return html;
        }
        #endregion

        #region 压缩HTML输出
        /// <summary>
        /// 压缩HTML输出
        /// </summary>
        public string ZipHtml(string Html)
        {
            Html = Regex.Replace(Html, @">\s+?<", "><");//去除HTML中的空白字符
            Html = Regex.Replace(Html, @"\r\n\s*", "");
            Html = Regex.Replace(Html, @"<body([\s\S]*?)>([\s\S]*?)</body>", @"<body$1>$2</body>", RegexOptions.IgnoreCase);
            return Html;
        }
        #endregion

        #region 过滤指定HTML标签
        /// <summary>
        /// 过滤指定HTML标签
        /// </summary>
        /// <param name="s_TextStr">要过滤的字符</param>
        /// <param name="html_Str">a img p div</param>
        public string DelHtml(string s_TextStr, string html_Str)
        {
            string rStr = "";
            if (!string.IsNullOrEmpty(s_TextStr))
            {
                rStr = Regex.Replace(s_TextStr, "<" + html_Str + "[^>]*>", "", RegexOptions.IgnoreCase);
                rStr = Regex.Replace(rStr, "</" + html_Str + ">", "", RegexOptions.IgnoreCase);
            }
            return rStr;
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

        /// <summary>
        /// 获取危害等级和安全攻击类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static HarmLevelEnum GetHarmLevelAndSafeAttackType(string input, out SafeAttackEnum attackType)
        {
            HarmLevelEnum levelEnum = HarmLevelEnum.Normal;
            attackType = SafeAttackEnum.Other;



            //验证的同时判断攻击类型和危害等级



            int i = 0;

            if (i == 0)
            {
                levelEnum = HarmLevelEnum.Normal;//安全
            }
            else if (i <= 3)
            {
                levelEnum = HarmLevelEnum.Bullet;//轻微
            }
            else if (i <= 7)
            {
                levelEnum = HarmLevelEnum.Missiles;//中等
            }
            else if (i > 7)
            {
                levelEnum = HarmLevelEnum.Nuke;//严重
            }
            return levelEnum;
        }
    }
}
