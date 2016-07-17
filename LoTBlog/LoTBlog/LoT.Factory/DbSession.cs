using LoT.Dal;
using LoT.IDal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace LoT.Factory
{
    /// <summary>
    /// 数据会话层~相当于工厂的工厂（实现业务层和数据层解耦）
    /// </summary>
    public partial class DbSession : IDbSession
    {
        #region 批量提交 ~ SaveChanges
        /// <summary>
        /// 批量提交
        /// 上下文中的SaveChanges提高到了DbSession层面中
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            DbContext context = EFContextFactory.GetEFContext();
            return context.SaveChanges();
        }
        #endregion

        #region 属性封装

        #region UserRegInfoDal
        private IUserRegInfoDal _userRegInfoDal;
        public IUserRegInfoDal UserRegInfoDal
        {
            get
            {
                if (_userRegInfoDal == null)
                {
                    _userRegInfoDal = DalsFactory.GetUserRegInfoDal();//通过工厂来获取对象从而实现与数据层的解耦
                }
                return _userRegInfoDal;
            }
        }
        #endregion

        #region ArticleDal
        private IArticleDal _articleDal;
        public IArticleDal ArticleDal
        {
            get
            {
                if (_articleDal == null)
                {
                    _articleDal = DalsFactory.GetArticleDal();//通过工厂来获取对象从而实现与数据层的解耦
                }
                return _articleDal;
            }
        }
        #endregion

        #region ArticleDisPhotoDal
        private IArticleDisPhotoDal _articleDisPhotoDal;
        public IArticleDisPhotoDal ArticleDisPhotoDal
        {
            get
            {
                if (_articleDisPhotoDal == null)
                {
                    _articleDisPhotoDal = DalsFactory.GetArticleDisPhotoDal();//通过工厂来获取对象从而实现与数据层的解耦
                }
                return _articleDisPhotoDal;
            }
        }
        #endregion        

        #region ArticleTypeDal
        private IArticleTypeDal _articleTypeDal;
        public IArticleTypeDal ArticleTypeDal
        {
            get
            {
                if (_articleTypeDal == null)
                {
                    _articleTypeDal = DalsFactory.GetArticleTypeDal();//通过工厂来获取对象从而实现与数据层的解耦
                }
                return _articleTypeDal;
            }
        }
        #endregion

        #region ArticleTagDal
        private IArticleTagDal _articleTagDal;
        public IArticleTagDal ArticleTagDal
        {
            get
            {
                if (_articleTagDal == null)
                {
                    _articleTagDal = DalsFactory.GetArticleTagDal();//通过工厂来获取对象从而实现与数据层的解耦
                }
                return _articleTagDal;
            }
        }
        #endregion

        #region BaseInfoDal
        private IBaseInfoDal _baseInfoDal;
        public IBaseInfoDal BaseInfoDal
        {
            get
            {
                if (_baseInfoDal == null)
                {
                    _baseInfoDal = DalsFactory.GetBaseInfoDal();//通过工厂来获取对象从而实现与数据层的解耦
                }
                return _baseInfoDal;
            }
        }
        #endregion

        #region SeoTKDDal
        private ISeoTKDDal _seoTKDDal;
        public ISeoTKDDal SeoTKDDal
        {
            get
            {
                if (_seoTKDDal == null)
                {
                    _seoTKDDal = DalsFactory.GetSeoTKDDal();//通过工厂来获取对象从而实现与数据层的解耦
                }
                return _seoTKDDal;
            }
        }
        #endregion

        #region FriendLinkDal
        private IFriendLinkDal _friendLinkDal;
        public IFriendLinkDal FriendLinkDal
        {
            get
            {
                if (_friendLinkDal == null)
                {
                    _friendLinkDal = DalsFactory.GetFriendLinkDal();//通过工厂来获取对象从而实现与数据层的解耦
                }
                return _friendLinkDal;
            }
        }
        #endregion

        #region QQModelDal
        private IQQModelDal _qQModelDal;
        public IQQModelDal QQModelDal
        {
            get
            {
                if (_qQModelDal == null)
                {
                    _qQModelDal = DalsFactory.GetQQModelDal();//通过工厂来获取对象从而实现与数据层的解耦
                }
                return _qQModelDal;
            }
        }
        #endregion

        #region PeopleDisPhotoDal
        private IPeopleDisPhotoDal _peopleDisPhotoDal;
        public IPeopleDisPhotoDal PeopleDisPhotoDal
        {
            get
            {
                if (_peopleDisPhotoDal == null)
                {
                    _peopleDisPhotoDal = DalsFactory.GetPeopleDisPhotoDal();//通过工厂来获取对象从而实现与数据层的解耦
                }
                return _peopleDisPhotoDal;
            }
        }
        #endregion

        #region AdvertisementDal
        private IAdvertisementDal _advertisementDal;
        public IAdvertisementDal AdvertisementDal
        {
            get
            {
                if (_advertisementDal == null)
                {
                    _advertisementDal = DalsFactory.GetAdvertisementDal();//通过工厂来获取对象从而实现与数据层的解耦
                }
                return _advertisementDal;
            }
        }
        #endregion

        #region ImgFlashDal
        private IImgFlashDal _imgFlashDal;
        public IImgFlashDal ImgFlashDal
        {
            get
            {
                if (_imgFlashDal == null)
                {
                    _imgFlashDal = DalsFactory.GetImgFlashDal();//通过工厂来获取对象从而实现与数据层的解耦
                }
                return _imgFlashDal;
            }
        }
        #endregion

        #region TalkingDal
        private ITalkingDal _talkingDal;
        public ITalkingDal TalkingDal
        {
            get
            {
                if (_talkingDal == null)
                {
                    _talkingDal = DalsFactory.GetTalkingDal();//通过工厂来获取对象从而实现与数据层的解耦
                }
                return _talkingDal;
            }
        }
        #endregion
        
        #endregion
    }
}
