using Dapper.Contrib.Extensions;

namespace PawChina.Model
{
    /// <summary>
    /// 笔记默认展图
    /// </summary>
    [Table("NoteDisPlayImg")]
    public class NoteDisPlayImg
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int DId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string DTitle { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string DPicUrl { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public StatusEnum DataStatus { get; set; }
    }
}