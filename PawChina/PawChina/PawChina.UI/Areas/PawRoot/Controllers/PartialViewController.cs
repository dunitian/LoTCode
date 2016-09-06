using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using LoTLib.Word.Split;
using PawChina.Model;

namespace PawChina.UI.Areas.PawRoot.Controllers
{
    public class PartialViewController : BaseController
    {
        #region 图片上传
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        public JsonResult Upload(HttpPostedFileBase file)
        {
            if (file == null) { return Json(new { status = false, msg = "图片提交失败" }); }
            if (file.ContentLength > 10485760) { return Json(new { status = false, msg = "文件10M以内" }); }
            string filterStr = ".gif,.jpg,.jpeg,.bmp,.png";
            string fileExt = Path.GetExtension(file.FileName).ToLower();
            if (!filterStr.Contains(fileExt)) { return Json(new { status = false, msg = "图片格式不对" }); }
            //防止黑客恶意绕过，判断下文件头文件
            if (!file.InputStream.CheckingExt())
            {
                //todo：一次危险记录
                return Json(new { status = false, msg = "图片格式不对" });
            }
            //todo: md5判断一下文件是否已经上传过,如果已经上传直接返回 return Json(new { status = true, msg = sqlPath });

            string path = string.Format("{0}/{1}", "/lotFiles", DateTime.Now.ToString("yyyy-MM-dd"));
            string fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), fileExt);
            string sqlPath = string.Format("{0}/{1}", path, fileName);
            string dirPath = Request.MapPath(path);

            if (!Directory.Exists(dirPath)) { Directory.CreateDirectory(dirPath); }
            try
            {
                //todo：缩略图
                file.SaveAs(Path.Combine(dirPath, fileName));
                file.InputStream.Dispose();
                //todo: 未来写存数据库的Code
            }
            catch { return Json(new { status = false, msg = "图片保存失败" }); }
            return Json(new { status = true, msg = sqlPath });
        }
        #endregion

        #region 结巴分词
        /// <summary>
        /// 结巴分词
        /// </summary>
        /// <param name="content">内容分词</param>
        /// <returns></returns>
        public JsonResult JieBaSplit(string content)
        {
            AjaxOption<object> obj = new AjaxOption<object>();
            try
            {
                content = content.GetArticleKeywordStr();
                obj.Status = true;
                obj.Msg = content;
            }
            catch
            {
                obj.Msg = "分词出了点小问题，您可以再试一下，或者手动分下~";
                return Json(obj);
            }
            return Json(obj);
        }
        #endregion
    }
}