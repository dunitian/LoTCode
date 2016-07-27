using Dapper.Contrib.Extensions;

namespace PawChina.Model
{
    [Table("ActivityInfo")]
    public class ActivityInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
		public int AId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
		public string ATitle { get; set; }
        /// <summary>
        /// 英文标题
        /// </summary>
		public string ATitleEn { get; set; }
        /// <summary>
        /// 活动内容
        /// </summary>
        public string AContent { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
		public System.DateTime ACreateTime { get; set; }
        /// <summary>
        /// 默认展图
        /// </summary>
		public string ADisplayPic { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
		public StatusEnum ADataStatus { get; set; }
    }
}
