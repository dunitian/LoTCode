using LoT.IDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LoT.Factory
{
    /// <summary>
    /// 实现DbSession与数据层的解耦
    /// </summary>
    public partial class DalsFactory
    {
        /// <summary>
        /// 抽象工厂公共方法
        /// </summary>
        /// <param name="dalName"></param>
        /// <returns></returns>
        public static object GetDal(string dalName)
        {
            //从配置文件中读取类的名字和程序集的名字
            string str = System.Configuration.ConfigurationManager.AppSettings[dalName];
            string className = str.Split(',')[0];
            string assemblyName = str.Split(',')[1];

            //获取程序集对象
            Assembly assembly = Assembly.Load(assemblyName);

            //创建对象实例
            return assembly.CreateInstance(className);
        }

        #region 获取Dal对象
        /// <summary>
        /// 返回UserRegInfoDal对象
        /// </summary>
        /// <returns></returns>
        public static IUserRegInfoDal GetUserRegInfoDal()
        {
            //简单工厂：return new UserRegInfoDal();
            //用抽象工厂或者Spring.Net实现
            return GetDal("UserRegInfoDal") as IUserRegInfoDal;
        }

        /// <summary>
        /// 返回ArticleDal对象
        /// </summary>
        /// <returns></returns>
        public static IArticleDal GetArticleDal()
        {
            return GetDal("ArticleDal") as IArticleDal;
        }

        /// <summary>
        /// 返回ArticleDisPhotoDal对象
        /// </summary>
        /// <returns></returns>
        public static IArticleDisPhotoDal GetArticleDisPhotoDal()
        {
            return GetDal("ArticleDisPhotoDal") as IArticleDisPhotoDal;
        }

        /// <summary>
        /// 返回ArticleTypeDal对象
        /// </summary>
        /// <returns></returns>
        public static IArticleTypeDal GetArticleTypeDal()
        {
            return GetDal("ArticleTypeDal") as IArticleTypeDal;
        }

        /// <summary>
        /// 返回ArticleTagDal对象
        /// </summary>
        /// <returns></returns>
        public static IArticleTagDal GetArticleTagDal()
        {
            return GetDal("ArticleTagDal") as IArticleTagDal;
        }

        /// <summary>
        /// 返回SeoTKDDal对象
        /// </summary>
        /// <returns></returns>
        public static ISeoTKDDal GetSeoTKDDal()
        {
            return GetDal("SeoTKDDal") as ISeoTKDDal;
        }

        /// <summary>
        /// 返回FriendLinkDal对象
        /// </summary>
        /// <returns></returns>
        public static IFriendLinkDal GetFriendLinkDal()
        {
            return GetDal("FriendLinkDal") as IFriendLinkDal;
        }

        /// <summary>
        /// 返回QQModelDal对象
        /// </summary>
        /// <returns></returns>
        public static IQQModelDal GetQQModelDal()
        {
            return GetDal("QQModelDal") as IQQModelDal;
        }

        /// <summary>
        /// 返回PeopleDisPhotoDal对象
        /// </summary>
        /// <returns></returns>
        public static IPeopleDisPhotoDal GetPeopleDisPhotoDal()
        {
            return GetDal("PeopleDisPhotoDal") as IPeopleDisPhotoDal;
        }

        /// <summary>
        /// 返回AdvertisementDal对象
        /// </summary>
        /// <returns></returns>
        public static IAdvertisementDal GetAdvertisementDal()
        {
            return GetDal("AdvertisementDal") as IAdvertisementDal;
        }

        /// <summary>
        /// 返回ImgFlashDal对象
        /// </summary>
        /// <returns></returns>
        public static IImgFlashDal GetImgFlashDal()
        {
            return GetDal("ImgFlashDal") as IImgFlashDal;
        }

        /// <summary>
        /// 返回TalkingDal对象
        /// </summary>
        /// <returns></returns>
        public static ITalkingDal GetTalkingDal()
        {
            return GetDal("TalkingDal") as ITalkingDal;
        }

        /// <summary>
        /// 返回BaseInfoDal对象
        /// </summary>
        /// <returns></returns>
        public static IBaseInfoDal GetBaseInfoDal()
        {
            return GetDal("BaseInfoDal") as IBaseInfoDal;
        }

        #endregion
    }
}
