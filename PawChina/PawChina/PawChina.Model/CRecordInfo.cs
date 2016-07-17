namespace PawChina.Model
{
	public class CRecordInfo
	{
        /// <summary>
        /// GUID
        /// </summary>
		public string RGuid { get; set; }
        /// <summary>
        /// 访问者的IP地址
        /// </summary>
		public string RIP { get; set; }
        /// <summary>
        /// 访问者的城市
        /// </summary>
		public string RCity { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
		public StatusEnum RStatus { get; set; }
	}
}
