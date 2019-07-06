using System;

namespace Aju.Carefree.Common
{
    public class Units
    {
        public static string GUID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
