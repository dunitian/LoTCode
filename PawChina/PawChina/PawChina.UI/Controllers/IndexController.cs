using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PawChina.UI.Controllers
{
    public class IndexController : Controller
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 600)]
        public async Task<ActionResult> Index()
        {
            var baseInfoList = await DapperDataAsync.QueryAsync<Models.PawBaseInfo>("select BId,BName,BValueA,BValueB,BSort from PawBaseInfo where BPage=@BPage and BDataStatus<>99", new { BPage = "IndexH" });
            if (baseInfoList.ExistsData())
            {
                ViewBag.PawTitle = baseInfoList.Where(b => b.BName == "PawTitle").FirstOrDefault();
                ViewBag.NewNote = baseInfoList.Where(b => b.BName == "NewNote").FirstOrDefault();
                ViewBag.NewProduct = baseInfoList.Where(b => b.BName == "NewProduct").FirstOrDefault();
                ViewBag.ProductType = baseInfoList.Where(b => b.BName == "ProductType").FirstOrDefault();
                ViewBag.ProductPart = baseInfoList.Where(b => b.BName == "ProductPart").FirstOrDefault();
            }

            //顶级分类 + 二级分类
            var proTypeInfoList = await DapperDataAsync.QueryAsync<Models.ProTypeInfo>("select TId,TName,TPid,TFloor,TSort from ProTypeInfo where TGroupType=@TGroupType and TFloor<>3 and TDataStatus<>99 ", new { TGroupType = 1 });
            if (proTypeInfoList.ExistsData())
            {
                ViewBag.ProTypeList = proTypeInfoList;
            }
            return View();
        }
    }
}
