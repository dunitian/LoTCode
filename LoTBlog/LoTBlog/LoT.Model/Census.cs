namespace LoT.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// 统计表（可以设置定时任务，每天12.00计算一次【有了统计表就不用每次计算节约了资源】）
    /// </summary>
    [Table("Census")]
    public partial class Census
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 名称（27字以内）
        /// </summary>
        [Required]
        [StringLength(27)]
        public string Name { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 状态（99为删除）
        /// </summary>
        public LoT.Enums.StatusEnum Status { get; set; }
    }
}
