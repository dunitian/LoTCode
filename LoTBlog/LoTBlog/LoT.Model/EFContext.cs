namespace LoT.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class EFContext : DbContext
    {
        public EFContext()
            : base("name=EFContext")
        {
        }

        #region DbSet<T>
        public virtual DbSet<ActionInfo> ActionInfo { get; set; }
        public virtual DbSet<AdminRecord> AdminRecord { get; set; }
        public virtual DbSet<Advertisement> Advertisement { get; set; }        
        public virtual DbSet<Article> Articel { get; set; }
        public virtual DbSet<ArticleDisPhoto> ArticleDisPhoto { get; set; }
        public virtual DbSet<ArticleTag> ArticleTag { get; set; }
        public virtual DbSet<ArticleType> ArticleType { get; set; }
        public virtual DbSet<BaseInfo> BaseInfo { get; set; }
        public virtual DbSet<Census> Census { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<DDos> DDos { get; set; }
        public virtual DbSet<DntRootInfo> DntRootInfo { get; set; }
        public virtual DbSet<FriendLink> FriendLink { get; set; }
        public virtual DbSet<HotWord> HotWord { get; set; }
        public virtual DbSet<ImgFlash> ImgFlash { get; set; }
        public virtual DbSet<PeopleDisPhoto> PeopleDisPhoto { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<QQModel> QQModel { get; set; }
        public virtual DbSet<PhotoType> PhotoType { get; set; }
        public virtual DbSet<RoleGroup> RoleGroup { get; set; }
        public virtual DbSet<RootAndAction> RootAndAction { get; set; }
        public virtual DbSet<SeoTKD> SeoTKD { get; set; }
        public virtual DbSet<Talking> Talking { get; set; }
        public virtual DbSet<UserRegInfo> UserRegInfo { get; set; }
        public virtual DbSet<XSS> XSS { get; set; }
        #endregion

        /// <summary>
        /// 模型创建方法
        /// </summary>
        /// <param name="modelBuilder">模型创建对象</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//移除表名复数  
        }
    }
}
