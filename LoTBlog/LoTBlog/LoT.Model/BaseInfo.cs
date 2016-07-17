namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 基础表
    /// </summary>
    [Table("BaseInfo")]
    public partial class BaseInfo
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 头部标题（不超过29个字）
        /// </summary>
        [Required]
        [StringLength(29)]
        public string TopTitle { get; set; }

        /// <summary>
        /// 头部简介（不超过100个字）
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TopText { get; set; }

        /// <summary>
        /// 头部Logo正面(相对路径路径)
        /// </summary>
        [Required]
        [StringLength(300)]
        public string TopLogoOne { get; set; }

        /// <summary>
        /// 头部Logo反面(相对路径路径)
        /// </summary>
        [Required]
        [StringLength(300)]
        public string TopLogoTwo { get; set; }

        /// <summary>
        /// 右侧头像（相对路径）
        /// </summary>
        [Required]
        [StringLength(300)]
        public string RightImg { get; set; }

        /// <summary>
        /// 右侧标题（不超过29个字）
        /// </summary>
        [Required]
        [StringLength(29)]
        public string RightTitle { get; set; }

        /// <summary>
        /// 右侧宣言（不超过29个字）
        /// </summary>
        [Required]
        [StringLength(29)]
        public string Manifesto { get; set; }

        /// <summary>
        /// 网名昵称（不超过29个字）
        /// </summary>
        [Required]
        [StringLength(29)]
        public string Nickname { get; set; }

        /// <summary>
        /// 奋斗目标（不超过29个字）
        /// </summary>
        [Required]
        [StringLength(29)]
        public string Goal { get; set; }

        /// <summary>
        /// 你的梦想（不超过29个字）
        /// </summary>
        [Required]
        [StringLength(29)]
        public string Dream { get; set; }

        /// <summary>
        /// 你的QQ号（int类型的数字）
        /// </summary>
        public int QQ { get; set; }

        /// <summary>
        /// 你的邮箱（不带中文的邮箱）
        /// </summary>
        [Required]
        [StringLength(29)]
        public string Email { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }

        #region 待删
        ///// <summary>
        ///// LoTBlog的版本号(英文_数字_不超过25位)
        ///// </summary>
        //[Required]
        //[StringLength(25)]
        //public string LoTVersion { get; set; }

        ///// <summary>
        ///// 作者名称
        ///// </summary>
        //[Required]
        //[StringLength(15)]
        //public string DntName { get; set; }

        ///// <summary>
        ///// 联系作者（一个邮箱发送的链接）
        ///// </summary>
        //[Required]
        //[StringLength(50)]
        //public string LinkDnt { get; set; } 
        #endregion

    }
}
