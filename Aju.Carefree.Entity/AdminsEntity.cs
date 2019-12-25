using Aju.Carefree.NetCore.BaseModel;
using SqlSugar;
using System;
namespace Aju.Carefree.Entity
{
    /// <summary>
    /// 管理员表
    /// </summary>
    [SugarTable("Sys_Admin")]
    public class AdminsEntity : BaseEntity<AdminsEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public string Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 账户名称
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public string DeptmentId { get; set; }

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
