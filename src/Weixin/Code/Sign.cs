using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Weixin.Code
{
    /// <summary>
    /// 签名类
    /// </summary>
    public class Sign
    {
        /// <summary>
        /// 根据签名算法生成签名
        /// </summary>
        /// <param name="parm">传递的参数键值对</param>
        /// <returns></returns>
        public static string GetSign(Dictionary<string, string> parm)
        {
            string stringSignTemp = "";
            string[] stringSignTempArr = new string[parm.Keys.Count];
            int parmi = 0;
            foreach (string str in parm.Keys)
            {
                stringSignTempArr[parmi] = str;
                parmi++;
            }
            //数组排序
            SortByASCII(stringSignTempArr);
            //拼接参数
            foreach (var item in stringSignTempArr)
            {
                if (string.IsNullOrEmpty(stringSignTemp))
                {
                    stringSignTemp = item + "=" + parm[item].ToString();
                }
                else
                {
                    stringSignTemp = stringSignTemp + "&" + item + "=" + parm[item].ToString();
                }
            }
            //拼接API密钥
            stringSignTemp = stringSignTemp + "&key=" + ConfigurationManager.AppSettings["mch_Secret"];
            //post的数据要求是utf8
            byte[] result = Encoding.UTF8.GetBytes(stringSignTemp);
            //MD5加密
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            //转成大写字符串,BitConverter会将字节数组转化为16进制字符串,并且用"-"连接
            string Sign = BitConverter.ToString(output).Replace("-","").ToUpper();
            return Sign;
        }

        /// <summary>
        /// 按照ASCII字符排序
        /// </summary>
        /// <param name="stringSignTempArr"></param>
        private static void SortByASCII(string[] arrayStr)
        {
            //获取最短字符串的长度
            int minStrLength = 100;
            foreach (var item in arrayStr)
            {
                if (item.Length < minStrLength)
                {
                    minStrLength = item.Length;
                }
            }
            //按照ASCII字符排序
            for (int i = 0; i < arrayStr.Length; i++)
            {
                for (int k = i + 1; k < arrayStr.Length; k++)
                {
                    int j = 0;
                    byte t = Encoding.ASCII.GetBytes(arrayStr[i])[j];
                    byte t1 = Encoding.ASCII.GetBytes(arrayStr[k])[j];

                    //先比较首字符，如果相等则比较后面字符
                    while (j < minStrLength && t == t1)
                    {
                        j++;
                        t = Encoding.ASCII.GetBytes(arrayStr[i])[j];
                        t1 = Encoding.ASCII.GetBytes(arrayStr[k])[j];
                    }
                    //替换排序
                    if (t > t1)
                    {
                        j = 0;
                        string temp = arrayStr[i];
                        arrayStr[i] = arrayStr[k];
                        arrayStr[k] = temp;
                    }
                }
            }
        }


    }
}