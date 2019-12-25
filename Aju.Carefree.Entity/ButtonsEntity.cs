using Aju.Carefree.NetCore.BaseModel;
using SqlSugar;
using System;
namespace Aju.Carefree.Entity
{
    /// <summary>
    /// 菜单按钮表
    /// </summary>
    [SugarTable("Sys_Buttons")]
    public class ButtonsEntity : BaseEntity<ButtonsEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public string ParentId { get; set; } = "0";

        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string EnCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; } = true;

        /// <summary>
        /// 事件方法名称 如：btn_add()
        /// </summary>
        public string EventFunName { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int Sort { get; set; }

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
