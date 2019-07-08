using System;

namespace Aju.Carefree.NetCore.BaseModel
{
    public interface IModificationAudited
    {
        string Id { get; set; }
        /// <summary>
        /// 最后修改用户
        /// </summary>
        string LastModifyUserId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime? LastModifyTime { get; set; }
    }
}
