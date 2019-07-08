using Aju.Carefree.NetCore.Helpers;
using System;
using System.Threading.Tasks;

namespace Aju.Carefree.NetCore.BaseModel
{
    public class BaseEntity<TEntity>
    {
        public async Task Create()
        {
            var entity = this as ICreationAudited;
            entity.Id = UtilHelper.GetGUID();
            var loginInfo = await OperatorProviderHelper.Provider.GetCurrent();
            if (loginInfo != null)
                entity.CreatorUserId = loginInfo.UserId;
            entity.CreatorTime = DateTime.Now;
        }

        public async Task Modify(string keyValue)
        {
            var entity = this as IModificationAudited;
            entity.Id = keyValue;
            var LoginInfo = await OperatorProviderHelper.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.LastModifyUserId = LoginInfo.UserId;
            }
            entity.LastModifyTime = DateTime.Now;
        }
        public async Task Remove()
        {
            var entity = this as IDeleteAudited;
            var LoginInfo = await OperatorProviderHelper.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.DeleteUserId = LoginInfo.UserId;
            }
            entity.DeleteTime = DateTime.Now;
            entity.DeleteMark = true;
        }
    }
}
