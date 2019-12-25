using Aju.Carefree.NetCore.BaseModel;
using SqlSugar;
using System;

namespace Aju.Carefree.Entity
{
    /// <summary>
    /// 菜单表（模块表）
    /// </summary>
    [SugarTable("Sys_Menus")]
    public class MenusEntity : BaseEntity<MenusEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public string Id { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public string ParentId { get; set; } = "0";

        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string ActionUrl { get; set; }

        /// <summary>
        /// 图标 icon 
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnabled { get; set; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? DeleteMark { get; set; } = false;
        /// <summary>
        /// 删除用户ID
        /// </summary>
        public string DeleteUserId { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 修改用户ID
        /// </summary>
        public string LastModifyUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
