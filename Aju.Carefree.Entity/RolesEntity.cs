using Aju.Carefree.NetCore.BaseModel;
using System;
using SqlSugar;
namespace Aju.Carefree.Entity
{
    /// <summary>
    /// 角色表
    /// </summary>
    [SugarTable("Sys_Role")]
    public class RolesEntity : BaseEntity<RolesEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; } = true;

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
