﻿using SevenTiny.Bantina.Bankinate.Attributes;
using SevenTiny.Cloud.MultiTenantPlatform.Domain.Enum;

namespace SevenTiny.Cloud.MultiTenantPlatform.Domain.Entity
{
    public class MetaField : MetaObjectManageInfo
    {
        //=DataType
        [Column]
        public int FieldType { get; set; }
        //if field type is datasource
        [Column]
        public int DataSourceId { get; set; } = -1;
        [Column]
        public int IsMust { get; set; }
        [Column]
        public int IsSystem { get; set; } = (int)TrueFalse.False;
    }
}
