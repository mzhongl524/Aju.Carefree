using Aju.Carefree.NetCore.Cache;
using System;
using System.Threading.Tasks;

namespace Aju.Carefree.NetCore.Helpers
{
    public class OperatorProviderHelper
    {
        private static OperatorProviderHelper _operatorProviderHelper = new OperatorProviderHelper();
        public static OperatorProviderHelper Instance { get { return _operatorProviderHelper; } }

        private static readonly string _operatorCacheKey = "Aju_Prince_OperatorProvider_20190708";

        public int Expiration { get; set; } = 10 * 60;
        public async Task AddCurrent(OperatorModel operatorModel)
        {
            await DistributedCacheManager.SetAsync(_operatorCacheKey, operatorModel, Expiration);
        }
        public async Task<OperatorModel> GetCurrent()
        {
            var result = await DistributedCacheManager.GetAsync<OperatorModel>(_operatorCacheKey);
            return result;
            // return (OperatorModel)ByteConvertHelper.Bytes2Object(result);
        }
        public async Task RemoveCurrent()
        {
            await DistributedCacheManager.RemoveAsync(_operatorCacheKey);
        }
    }

    public class OperatorModel
    {
        public string UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string CompanyId { get; set; }
        public string DepartmentId { get; set; }
        public string RoleId { get; set; }
        public string LoginIPAddress { get; set; }
        public string LoginIPAddressName { get; set; }
        public string LoginToken { get; set; }
        public DateTime LoginTime { get; set; }
        public bool IsSystem { get; set; }
        public string OrgId { get; set; }
    }
}
