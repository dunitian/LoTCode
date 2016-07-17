using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace LoTBlog.Back.Models
{
    /// <summary>
    /// Ajax请求响应类
    /// </summary>
    /// <typeparam name="T">指定响应的数据类型</typeparam>
    public partial class AjaxResponse<T> where T : class
    {
        /// <summary>
        /// 获取或设置是否响应成功属性
        /// </summary>
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 获取或设置错误码属性
        /// </summary>
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 获取或设置错误消息属性
        /// </summary>
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 获取或设置响应数据属性
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }

        /// <summary>
        /// 获取或设置响应数据属性
        /// </summary>
        [JsonProperty("otherData")]
        public object OtherData { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AjaxResponse()
        {
            IsSuccess = false;
            ErrorCode = null;
            ErrorMessage = null;
            Data = default(T);
            OtherData = null;
        }
    }
}