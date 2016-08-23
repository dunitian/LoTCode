using Dapper.Contrib.Extensions;

namespace PawChina.Model
{
    /// <summary>
    /// 12个字段
    /// </summary>
    [Table("NoteInfo")]
    public class NoteInfo
    {
        /// <summary>
        /// Row序号（已经用动态类型查询，所以这个可以省去了）
        /// </summary>
        //public int Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int NId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string NTitle { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string NContent { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string NAuthor { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int NHitCount { get; set; }
        /// <summary>
        /// 创建时间轴
        /// </summary>
        public long NCreateTime { get; set; }
        /// <summary>
        /// 更新时间轴
        /// </summary>
        public long NUpdateTime { get; set; }
        /// <summary>
        /// 文章展图
        /// </summary>
        public string NDisplayPic { get; set; }
        /// <summary>
        /// 是否推送到主页
        /// </summary>
        public bool NPush { get; set; }
        /// <summary>
        /// SEO外键ID
        /// </summary>
        public int NSeoId { get; set; }
        /// <summary>
        /// SEOInfo
        /// </summary>
        [Write(false)]
        public SeoTKD SeoInfo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public StatusEnum NDataStatus { get; set; }
    }
}