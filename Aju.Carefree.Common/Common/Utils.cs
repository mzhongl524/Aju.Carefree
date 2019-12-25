using System.Security.Cryptography;
using System.Text;

namespace Aju.Carefree.Common.Common
{
    public class Utils
    {
        /// <summary>
        /// MD5加密函数
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>MD5加密后的结果</returns>
        public static string MD5(string str)
        {
            byte[] b = Encoding.UTF8.GetBytes(str);
            using (MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                b = mD5CryptoServiceProvider.ComputeHash(b);
            }
            string ret = string.Empty;
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }
    }
}
