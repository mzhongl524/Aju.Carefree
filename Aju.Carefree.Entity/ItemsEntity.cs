using Aju.Carefree.NetCore.BaseModel;
using SqlSugar;
using System;

namespace Aju.Carefree.Entity
{
    /// <summary>
    /// 字典
    /// </summary>
    [SugarTable("Sys_Items")]
    public class ItemsEntity : BaseEntity<ItemsEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        [SugarColumn(IsIdentity = false, IsPrimaryKey = true)]
        public string Id { get; set; }

        public string ParentId { get; set; }
        public string EnCode { get; set; }
        public string FullName { get; set; }
        public bool? IsTree { get; set; }
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
