namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 相册分类表
    /// </summary>
    [Table("PhotoType")]
    public partial class PhotoType
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        /// <summary>
        /// 简介（249字以内）
        /// </summary>
        [StringLength(249)]
        public string Introduction { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
