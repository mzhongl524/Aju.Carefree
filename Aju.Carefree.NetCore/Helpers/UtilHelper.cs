using System;

namespace Aju.Carefree.NetCore.Helpers
{
    public static class UtilHelper
    {
        public static string GetGUID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
