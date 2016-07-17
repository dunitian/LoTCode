namespace PawChina.Model
{
    public class ProductInfo
    {
        /// <summary>
        /// 产品编号
        /// </summary>
		public int PId { get; set; }
        /// <summary>
        /// 产品标题
        /// </summary>
		public string PTitle { get; set; }
        /// <summary>
        /// 产品内容
        /// </summary>
		public string PContent { get; set; }
        /// <summary>
        /// 排序 ~ 越大越靠前
        /// </summary>
		public short PSort { get; set; }
        /// <summary>
        /// 页面浏览量
        /// </summary>
		public int PHitCount { get; set; }
        /// <summary>
        /// 产品创建时间
        /// </summary>
		public System.DateTime PCreateTime { get; set; }
        /// <summary>
        /// 产品更新时间
        /// </summary>
		public System.DateTime PUpdateTime { get; set; }
        /// <summary>
        /// 产品类型，是产品还是配件
        /// </summary>
		public ProductEnum PGroupType { get; set; }
        /// <summary>
        /// 产品展图
        /// </summary>
        public string PDisplayPic { get; set; }
        /// <summary>
        /// 是否推送到主页
        /// </summary>
        public bool PPush { get; set; }
        /// <summary>
        /// Seo外键ID
        /// </summary>
        public int PSeoId { get; set; }
        /// <summary>
        /// 产品类型外键ID
        /// </summary>
        public int PTypeId { get; set; }
        /// <summary>
        /// 产品状态
        /// </summary>
		public StatusEnum PDataStatus { get; set; }
    }
}
