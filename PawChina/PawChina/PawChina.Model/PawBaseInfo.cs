using Dapper.Contrib.Extensions;

namespace PawChina.Model
{
    /// <summary>
    /// 首页信息
    /// </summary>
    [Table("NoteInfo")]
    public class PawBaseInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int BId { get; set; }
        /// <summary>
        /// key
        /// </summary>
        public string BName { get; set; }
        /// <summary>
        /// valueA
        /// </summary>
        public string BValueA { get; set; }
        /// <summary>
        /// valueB
        /// </summary>
        public string BValueB { get; set; }
        /// <summary>
        /// 所属页面
        /// </summary>
        public string BPage { get; set; }
        /// <summary>
        /// 序号，越大越靠前
        /// </summary>
        public short BSort { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public StatusEnum BDataStatus { get; set; }
    }
}