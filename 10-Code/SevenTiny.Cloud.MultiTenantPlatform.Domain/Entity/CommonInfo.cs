﻿using SevenTiny.Bantina.Bankinate;
using System;
using SevenTiny.Bantina.Bankinate.Attributes;

namespace SevenTiny.Cloud.MultiTenantPlatform.Domain.Entity
{
    /// <summary>
    /// EntityInfo Standard Property
    /// </summary>
    public class CommonInfo
    {
        [Key]
        [AutoIncrease]
        public int Id { get; set; }
        [Column("`Name`")]
        public string Name { get; set; }
        [Column("`Code`")]
        public string Code { get; set; }
        [Column("`Description`")]
        public string Description { get; set; } = string.Empty;
        [Column("`Group`")]
        public string Group { get; set; } = string.Empty;
        [Column]
        public int SortNumber { get; set; } = 0;
        [Column]
        public int IsDeleted { get; set; } = 0;
        [Column]
        public int CreateBy { get; set; } = -1;
        [Column("`CreateTime`")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
        [Column]
        public int ModifyBy { get; set; } = -1;
        [Column("`ModifyTime`")]
        public DateTime ModifyTime { get; set; } = DateTime.Now;
    }
}
