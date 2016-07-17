using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoT.IDal
{
    public interface IDbSession
    {
        #region 属性封装
        //IActionInfoDal ActionInfoDal { get; }
        //IAdminRecordDal AdminRecordDal { get; }
        IAdvertisementDal AdvertisementDal { get; }
        IArticleDal ArticleDal { get; }
        IArticleDisPhotoDal ArticleDisPhotoDal { get; }
        IArticleTagDal ArticleTagDal { get; }
        IArticleTypeDal ArticleTypeDal { get; }
        IBaseInfoDal BaseInfoDal { get; }
        //ICensusDal CensusDal { get; }
        //ICommentDal CommentDal { get; }
        //IDDosDal DDosDal { get; }
        //IDntRootInfoDal DntRootInfoDal { get; }
        IFriendLinkDal FriendLinkDal { get; }
        //IHotWordDal HotWordDal { get; }
        IImgFlashDal ImgFlashDal { get; }
        IQQModelDal QQModelDal { get; }
        IPeopleDisPhotoDal PeopleDisPhotoDal { get; }
        //IPhotoDal PhotoDal { get; }
        //IPhotoTypeDal PhotoTypeDal { get; }
        //IPublicRecordDal PublicRecordDal { get; }
        //IRoleGroupDal RoleGroupDal { get; }
        //IRootAndActionDal RootAndActionDal { get; }
        ISeoTKDDal SeoTKDDal { get; }
        ITalkingDal TalkingDal { get; }
        IUserRegInfoDal UserRegInfoDal { get; }
        //IXSSDal XSSDal { get; }
        #endregion

        #region 批量提交 ~ SaveChanges
        /// <summary>
        /// 批量提交
        /// 上下文中的SaveChanges提高到了DbSession层面中
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        #endregion
    }
}
