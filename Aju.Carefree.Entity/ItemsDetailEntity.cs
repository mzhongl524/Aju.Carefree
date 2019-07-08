using Aju.Carefree.NetCore.BaseModel;
using SqlSugar;
using System;

namespace Aju.Carefree.Entity
{
    /// <summary>
    /// 选项明细表
    /// </summary>
    [SugarTable("Sys_ItemsDetail")]
    public class ItemsDetailEntity : BaseEntity<ItemsDetailEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [SugarColumn(IsIdentity = false, IsPrimaryKey = true)]
        public string Id { get; set; }
        public string ItemId { get; set; }
        public string ParentId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string SimpleSpelling { get; set; }
        public bool? IsDefault { get; set; }
        public int? Layers { get; set; }
        public int? SortCode { get; set; }
        public bool? DeleteMark { get; set; }
        public bool? EnabledMark { get; set; }
        public string Description { get; set; }
        public DateTime? CreatorTime { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifyUserId { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string DeleteUserId { get; set; }
    }
}
