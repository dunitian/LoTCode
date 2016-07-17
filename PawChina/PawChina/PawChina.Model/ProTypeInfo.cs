namespace PawChina.Model
{
    public class ProTypeInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
		public int TId { get; set; }
        /// <summary>
        /// 分类名
        /// </summary>
		public string TName { get; set; }
        /// <summary>
        /// 分类简介
        /// </summary>
		public string TContent { get; set; }
        /// <summary>
        /// 父分类ID
        /// </summary>
        public int TPid { get; set; }
        /// <summary>
        /// 排序编号
        /// </summary>
		public short TSort { get; set; }
        /// <summary>
        /// 几级菜单
        /// </summary>
		public short TFloor { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
		public System.DateTime TCreateTime { get; set; }
        /// <summary>
        /// 分类类型
        /// </summary>
        public ProductEnum TGroupType { get; set; }
        /// <summary>
        /// 默认展图
        /// </summary>
		public string TDisplayPic { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
		public StatusEnum TDataStatus { get; set; }
    }
}
