using LoT.Enums;
using LoT.IService;
using LoT.Model;
using LoTBlog.Back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Back.Controllers
{
    /// <summary>
    /// 行业资讯管理,逆天笔记专栏,网络资源专栏
    /// </summary>
    public class ArticleController : Controller
    {
        public IArticleService ArticleService { get; set; }// 文章
        public IArticleDisPhotoService ArticleDisPhotoService { get; set; }// 文章
        public IArticleTagService ArticleTagService { get; set; }// 文章Tag
        public IArticleTypeService ArticleTypeService { get; set; }// 文章分类

        /// <summary>
        /// 行业资讯管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewData.Model = ArticleTypeService.PageLoad(at => at.Pid == 0 || at.Pid == null);
            return View();
        }

        /// <summary>
        /// 逆天笔记专栏
        /// </summary>
        /// <returns></returns>
        public ActionResult NotePad()
        {
            ViewData.Model = ArticleTypeService.PageLoad(at => at.Pid == 0 || at.Pid == null);
            return View();
        }

        /// <summary>
        /// 网络资源专栏
        /// </summary>
        /// <returns></returns>
        public ActionResult Resource()
        {
            ViewData.Model = ArticleTypeService.PageLoad(at => at.Pid == 0 || at.Pid == null);
            return View();
        }

        #region 公用方法

        /// <summary>
        /// 获取文章类型（以-分隔）
        /// </summary>
        /// <param name="typeIds">typeIds(,分隔)</param>
        /// <returns></returns>
        private string GetArticleType(string typeIds = "")
        {
            var typeIdList = typeIds.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(at => Convert.ToInt32(at));
            string[] typeNames = ArticleTypeService.PageLoad(at => typeIdList.Contains(at.Id)).Select(at => at.Name).ToArray();
            return string.Join("-", typeNames);
        }

        #region 添加文章页面
        /// <summary>
        /// 添加文章页面
        /// </summary>
        /// <param name="GroupType">文章类别（资讯1 笔记2 网络资源3）</param>
        /// <returns></returns>
        public ActionResult Add(int GroupType = 1)
        {
            ViewBag.ArticleTopType = ArticleTypeService.PageLoad(at => at.Status != StatusEnum.Delete && (at.Pid == 0 || at.Pid == null));
            ViewBag.GroupType = GroupType;
            ViewBag.ArticleTags = ArticleTagService.PageLoad(t => t.Status != StatusEnum.Delete);

            //后期可删除==>浏览量
            Random r = new Random();
            ViewBag.HitNum = r.Next(100, 1000);

            return View();
        }

        /// <summary>
        /// 添加文章页面
        /// </summary>
        /// <param name="Title">文章标题</param>
        /// <param name="Author">文章作者</param>
        /// <param name="TContent">文章内容</param>
        /// <param name="TypeIds">文章分类(typeId,分隔)【最多100个字符】</param>
        /// <param name="TagIds">文章Tag(tagid用,分隔)【最多100个字符】</param>
        /// <param name="GroupType">文章类别（资讯1 笔记2 网络资源3）</param>
        /// <param name="Recommend">推荐类型 0不推荐 1编辑推荐 2逆天推荐 3网友推荐</param>
        /// <param name="HitCount">浏览次数</param>
        /// <param name="Sort">排序，默认升序排列，0在最前面</param>
        /// <param name="Status">状态（0,所有人可见，1,好友可见，2,仅自己可见,99删除）</param>
        /// <param name="DisplayPic">文章默认展图</param>
        /// <param name="SeoKeywords">SeoKeyWords</param>
        /// <param name="Seodescription">Sedescription</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add(string Title = "", string Author = "", string TContent = "", string TypeIds = "", string TagIds = "", int GroupType = 1, int Recommend = 0, int HitCount = 9, int Sort = 9, int Status = 0, string DisplayPic = "", string SeoKeywords = "", string Seodescription = "")
        {
            AjaxResponse<object> obj = new AjaxResponse<object>();

            #region 一系列验证
            if (string.IsNullOrEmpty(Title))
            {
                obj.ErrorMessage = "文章标题不能为空！";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(Author))
            {
                obj.ErrorMessage = "作者不能为空！";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(TContent))
            {
                obj.ErrorMessage = "文章内容不能为空！";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(TypeIds))
            {
                obj.ErrorMessage = "请好好选择文章分类！";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(TagIds))
            {
                obj.ErrorMessage = "请好好选择文章Tag！";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(SeoKeywords))
            {
                obj.ErrorMessage = "Seo关键词不能为空！";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(Seodescription))
            {
                obj.ErrorMessage = "Seo头部描述能为空！";
                return Json(obj);
            }
            if (Title.Length > 50)
            {
                obj.ErrorMessage = "新闻标题50个字以内！";
                return Json(obj);
            }
            if (Author.Length > 15)
            {
                obj.ErrorMessage = "作者15个字以内！";
                return Json(obj);
            }
            if (SeoKeywords.Length > 149)
            {
                obj.ErrorMessage = "Seo头关键词149字以内！";
                return Json(obj);
            }
            if (Seodescription.Length > 249)
            {
                obj.ErrorMessage = "Seo头部描述249字以内！";
                return Json(obj);
            }
            if (TagIds.Length > 100)
            {
                obj.ErrorMessage = "文章Tag好像有点太多了啊！";
                return Json(obj);
            }
            if (TypeIds.Length > 100)
            {
                obj.ErrorMessage = "文章分类好像有点那个啥吧！";
                return Json(obj);
            }
            #endregion

            SeoTKD seoInfo = new SeoTKD()
            {
                SeoKeywords = SeoKeywords,
                Sedescription = Seodescription,
                Status = StatusEnum.Normal
            };

            #region 存储前的处理
            TypeIds = TypeIds.Trim(',');
            TagIds = TagIds.Trim(',');

            //传过来的已经url编码了【带敏感字符的url服务器默认是拒绝请求的】
            //没有url编码的，解码还是他本身
            TContent = HttpUtility.UrlDecode(TContent);
            //必须保证存在数据库里面的文字是安全的
            TContent = HttpUtility.HtmlEncode(TContent);

            //如果没有上传默认展图，就随机展示一个默认展图
            if (string.IsNullOrEmpty(DisplayPic))
            {
                IList<ArticleDisPhoto> disPics = ArticleDisPhotoService.PageLoad(p => p.Status != StatusEnum.Delete).ToList();
                int count = disPics.Count;
                if (count > 0)
                {
                    Random random = new Random();
                    int index = random.Next(disPics.Count);
                    DisplayPic = disPics[index].PicUrl;
                }
                else//实在没有的话就给一个默认值
                {
                    DisplayPic = LoT.Common.ConfigHelper.GetValueForConfigAppKey("ArticleTypeDisPlayPic");
                }
            }
            #endregion

            Article article = new Article()
            {
                Title = Title,
                Author = Author,
                TContent = TContent,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                TypeIds = TypeIds.Trim(','),
                GroupType = (GroupEnum)(GroupType == 0 ? 1 : GroupType),
                Recommend = (RecommendEnum)Recommend,
                HitCount = HitCount,
                Sort = Sort,
                TagIds = TagIds,
                Status = (ArticleStatusEnum)Status,
                DisplayPic = DisplayPic
            };

            int i = ArticleService.AddArticle(article, seoInfo);
            obj.IsSuccess = i > 0;
            if (i <= 0)
            {
                obj.ErrorMessage = "未知原因添加失败~";
            }
            return Json(obj);
        }
        #endregion

        #region 文章修改页面
        /// <summary>
        /// 文章修改页面
        /// </summary>
        /// <param name="GroupType">文章类别（资讯1 笔记2 网络资源3）</param>
        /// <returns></returns>
        public ActionResult Update(int GroupType = 1, int ArticleId = 0)
        {
            Article article = ArticleService.FindModel(ArticleId);
            //不存在就跳转到添加页面
            if (article == null)
            {
                return RedirectToAction("Add", new { GroupType = GroupType });
            }
            ViewBag.GroupType = GroupType;
            ViewBag.ArticleTags = ArticleTagService.PageLoad(t => t.Status != StatusEnum.Delete);

            #region 文章分类（三级）
            ViewBag.ArticleTopType = ArticleTypeService.PageLoad(at => at.Status != StatusEnum.Delete && (at.Pid == 0 || at.Pid == null));

            int[] ids = article.TypeIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToArray();
            if (ids.Length == 0)
            {
                return View(article);
            }
            int oneId;
            int twoId;
            int threeId;
            switch (ids.Length)
            {
                case 1:
                    oneId = ids[0];
                    ViewBag.TypeOneId = oneId;
                    break;
                case 2:
                    oneId = ids[0];
                    twoId = ids[1];
                    ViewBag.TypeOneId = oneId;
                    ViewBag.TypeTwoId = twoId;
                    ViewBag.TypeTwo = ArticleTypeService.PageLoad(at => at.Pid == oneId && at.Status != StatusEnum.Delete);
                    break;
                case 3:
                    oneId = ids[0];
                    twoId = ids[1];
                    threeId = ids[2];
                    ViewBag.TypeOneId = oneId;
                    ViewBag.TypeTwoId = twoId;
                    ViewBag.TypeThreeId = threeId;
                    ViewBag.TypeTwo = ArticleTypeService.PageLoad(at => at.Pid == oneId && at.Status != StatusEnum.Delete);
                    ViewBag.TypeThree = ArticleTypeService.PageLoad(at => at.Pid == twoId && at.Status != StatusEnum.Delete);
                    break;
            }
            #endregion

            return View(article);
        }

        /// <summary>
        /// 文章修改页面(小复杂的逻辑处理)
        /// </summary>
        /// <param name="ArticleId">文章ID</param>
        /// <param name="SeoId">SEO ID</param>
        /// <param name="CreateTime">创建时间</param>
        /// <param name="Title">文章标题</param>
        /// <param name="Author">文章作者</param>
        /// <param name="TContent">文章内容</param>
        /// <param name="TypeIds">文章分类(typeId,分隔)【最多100个字符】</param>
        /// <param name="TagIds">文章Tag(tagid用,分隔)【最多100个字符】</param>
        /// <param name="GroupType">文章类别（资讯1 笔记2 网络资源3）</param>
        /// <param name="Recommend">推荐类型 0不推荐 1编辑推荐 2逆天推荐 3网友推荐</param>
        /// <param name="HitCount">浏览次数</param>
        /// <param name="Sort">排序，默认升序排列，0在最前面</param>
        /// <param name="Status">状态（0,所有人可见，1,好友可见，2,仅自己可见,99删除）</param>
        /// <param name="DisplayPic">文章默认展图</param>
        /// <param name="SeoKeywords">SeoKeyWords</param>
        /// <param name="Seodescription">Sedescription</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(int ArticleId = 0, int SeoId = 0, string CreateTime = "", string Title = "", string Author = "", string TContent = "", string TypeIds = "", string TagIds = "", int GroupType = 1, int Recommend = 0, int HitCount = 9, int Sort = 9, int Status = 0, string DisplayPic = "", string SeoKeywords = "", string Seodescription = "")
        {
            AjaxResponse<object> obj = new AjaxResponse<object>();

            #region 一系列验证
            if (ArticleId == 0 || SeoId == 0)
            {
                obj.ErrorMessage = "文章或Seo不存在！";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(Title))
            {
                obj.ErrorMessage = "文章标题不能为空！";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(Author))
            {
                obj.ErrorMessage = "作者不能为空！";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(TContent))
            {
                obj.ErrorMessage = "文章内容不能为空！";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(TypeIds))
            {
                obj.ErrorMessage = "请好好选择文章分类！";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(TagIds))
            {
                obj.ErrorMessage = "请好好选择文章Tag！";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(SeoKeywords))
            {
                obj.ErrorMessage = "Seo关键词不能为空！";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(Seodescription))
            {
                obj.ErrorMessage = "Seo头部描述能为空！";
                return Json(obj);
            }
            if (Title.Length > 50)
            {
                obj.ErrorMessage = "新闻标题50个字以内！";
                return Json(obj);
            }
            if (Author.Length > 15)
            {
                obj.ErrorMessage = "作者15个字以内！";
                return Json(obj);
            }
            if (SeoKeywords.Length > 149)
            {
                obj.ErrorMessage = "Seo头关键词149字以内！";
                return Json(obj);
            }
            if (Seodescription.Length > 249)
            {
                obj.ErrorMessage = "Seo头部描述249字以内！";
                return Json(obj);
            }
            if (TagIds.Length > 100)
            {
                obj.ErrorMessage = "文章Tag好像有点太多了啊！";
                return Json(obj);
            }
            if (TypeIds.Length > 100)
            {
                obj.ErrorMessage = "文章分类好像有点那个啥吧！";
                return Json(obj);
            }
            #endregion

            SeoTKD seoInfo = new SeoTKD()
            {
                Id = SeoId,
                SeoKeywords = SeoKeywords,
                Sedescription = Seodescription,
                Status = StatusEnum.Normal
            };

            #region 存储前的处理
            TypeIds = TypeIds.Trim(',');
            TagIds = TagIds.Trim(',');
            DateTime timeTemp;
            DateTime.TryParse(CreateTime, out timeTemp);

            //传过来的已经url编码了【带敏感字符的url服务器默认是拒绝请求的】
            //没有url编码的，解码还是他本身
            TContent = HttpUtility.UrlDecode(TContent);
            //必须保证存在数据库里面的文字是安全的
            TContent = HttpUtility.HtmlEncode(TContent);

            //如果没有上传默认展图，就随机展示一个默认展图
            if (string.IsNullOrEmpty(DisplayPic))
            {
                IList<ArticleDisPhoto> disPics = ArticleDisPhotoService.PageLoad(p => p.Status != StatusEnum.Delete).ToList();
                int count = disPics.Count;
                if (count > 0)
                {
                    Random random = new Random();
                    int index = random.Next(disPics.Count);
                    DisplayPic = disPics[index].PicUrl;
                }
                else//实在没有的话就给一个默认值
                {
                    DisplayPic = LoT.Common.ConfigHelper.GetValueForConfigAppKey("ArticleTypeDisPlayPic");
                }
            }
            #endregion

            Article article = new Article()
            {
                Id = ArticleId,
                Title = Title,
                Author = Author,
                TContent = TContent,
                CreateTime = timeTemp,
                UpdateTime = DateTime.Now,
                TypeIds = TypeIds,
                GroupType = (GroupEnum)(GroupType == 0 ? 1 : GroupType),
                Recommend = (RecommendEnum)Recommend,
                HitCount = HitCount,
                Sort = Sort,
                TagIds = TagIds,
                Status = (ArticleStatusEnum)Status,
                DisplayPic = DisplayPic,
                SeoId = SeoId
            };

            bool b = ArticleService.UpdateArticle(article, seoInfo) > 0;
            obj.IsSuccess = b;
            if (!b)
            {
                obj.ErrorMessage = "未知原因添加失败~";
            }
            return Json(obj);
        }
        #endregion

        /// <summary>
        /// 文章查询
        /// </summary>
        /// <param name="GroupType">1 资讯，2 笔记，3 网络资源</param>
        /// <param name="page">当前页（1开始）</param>
        /// <param name="rows">每页显示多少条数据</param>
        /// <param name="Title">文章标题</param>
        /// <param name="TypeIds">文章所属分类(，分隔)</param>
        /// <param name="startime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <returns></returns>
        public JsonResult Query(int GroupType = 0, int page = 1, int rows = 20, string Title = "", string TypeIds = "0", string startime = "", string endtime = "")
        {
            DateTime regStartTime;
            DateTime regEndTime;
            if (!DateTime.TryParse(startime, out regStartTime))
            {
                regStartTime = DateTime.MinValue;
            }

            if (!DateTime.TryParse(endtime, out regEndTime))
            {
                regEndTime = DateTime.MaxValue;
            }

            int total = 0;
            Expression<Func<Article, bool>> whereLambada =
                a => (GroupType == 0 || a.GroupType == (GroupEnum)GroupType)
                    && (string.IsNullOrEmpty(Title) || a.Title.Contains(Title))
                    && (TypeIds == "0" || a.TypeIds == TypeIds || a.TypeIds.IndexOf(TypeIds) == 0)
                    && (startime == string.Empty || a.CreateTime >= regStartTime)
                    && (endtime == string.Empty || a.CreateTime <= regEndTime);

            var articleList = ArticleService.PageLoad(whereLambada, a => new { a.CreateTime }, true, page, rows, out total).ToList().Select(a => new ArticleTemp
            {
                Id = a.Id,
                Title = a.Title,
                ArticleType = GetArticleType(a.TypeIds),
                Sort = a.Sort,
                CreateTime = a.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                UpdateTime = a.UpdateTime.ToString("yyyy-MM-dd HH:mm"),
                Status = a.Status
            });

            var obj = new AjaxResponse<ListData<ArticleTemp>>();
            obj.IsSuccess = true;
            obj.Data = new ListData<ArticleTemp>();
            obj.Data.rows = articleList;
            obj.Data.total = total;
            return Json(obj);
        }

        /// <summary>
        /// 批量删除文章
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult DeletList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = ArticleService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = ArticleStatusEnum.Delete);
            AjaxResponse<ArticleType> obj = new AjaxResponse<ArticleType>();
            if (i > 0)
            {
                obj.IsSuccess = true;
                obj.OtherData = "成功删除" + i + "条记录";
            }
            else
            {
                obj.ErrorMessage = "操作失败！";
            }
            return Json(obj);
        }

        /// <summary>
        /// 批量恢复文章
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult RecoverList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = ArticleService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = ArticleStatusEnum.All);
            AjaxResponse<ArticleType> obj = new AjaxResponse<ArticleType>();
            if (i > 0)
            {
                obj.IsSuccess = true;
                obj.OtherData = "成功恢复" + i + "条记录";
            }
            else
            {
                obj.ErrorMessage = "操作失败！";
            }
            return Json(obj);
        }

        #endregion
    }
}
