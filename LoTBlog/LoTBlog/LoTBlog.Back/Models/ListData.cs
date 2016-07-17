using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace LoTBlog.Back.Models
{
    /// <summary>
    /// 列表数据对象
    /// </summary>
    /// <typeparam name="T">指定的数据类型</typeparam>
    public partial class ListData<T>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        [JsonProperty("total")]
        public int total { get; set; }

        /// <summary>
        /// 当前页记录集合
        /// </summary>
        [JsonProperty("rows")]
        public IEnumerable<T> rows { get; set; }
    }
}