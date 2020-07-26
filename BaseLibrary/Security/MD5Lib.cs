using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BaseLibrary.Security
{
    public static class MD5Lib
    {
        #region 取得MD5編碼
        /// <summary>
        /// 取得MD5編碼
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string ToMD5(this string Value)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(Value));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                Value = sBuilder.ToString();
            }

            return Value;
        }
        #endregion

        #region 取得大寫的MD5
        public static string ToMD5Upper(this string Value)
        {
            return Value.ToMD5().ToUpper();
        }
        #endregion
    }
}
