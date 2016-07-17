namespace PawChina.Model
{
    /// <summary>
    /// 管理员
    /// </summary>
    public class ChineseInfo
    {
        /// <summary>
        /// GUID
        /// </summary>
		public string PawGid { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
		public string PawName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
		public string PawPass { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
		public System.DateTime PawCreateTime { get; set; }
        /// <summary>
        /// 找回密码的邮箱
        /// </summary>
        public string PawEmail { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public StatusEnum PawDataStatus { get; set; }
    }
}
