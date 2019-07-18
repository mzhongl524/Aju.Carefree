using SqlSugar;
namespace Aju.Carefree.Entity
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    [SugarTable("Sys_RolePermission")]
    public class RolePermissionEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public string Id { get; set; }

        /// <summary>
        /// 模块类型
        /// </summary>
        public MoudelTypeEnum MoudelType { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public string ObjectId { get; set; }
    }

    public enum MoudelTypeEnum
    {
        /// <summary>
        /// 菜单
        /// </summary>
        Menu = 1,
        /// <summary>
        /// 按钮
        /// </summary>
        Button
    }
}
