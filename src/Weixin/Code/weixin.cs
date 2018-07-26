using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace Weixin
{
    public class weixin
    {
        public static string GetUserInfo(string[] paraArray,string type)
        {
            string url = "";
            string json = "";
            string appid = ConfigurationManager.AppSettings["AppID"];
            string secret = ConfigurationManager.AppSettings["AppSecret"];
            var client = new System.Net.WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            switch (type)
            {
                case "GetAccessToken":
                    url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, paraArray[0]);
                    break;
                case "GetUserInfo":
                    url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", paraArray[0], paraArray[1]);
                    break;
            }
            json = client.DownloadString(url);
            return json;            
        }
        
        /// <summary>
        /// PostHttp
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="body">内容</param>
        /// <param name="contentType">类型</param>
        /// <param name="cert">证书</param>
        /// <returns></returns>
        public static string PostHttp(string url, string body)
        {
            //添加证书
            string certpath = ConfigurationManager.AppSettings["cert_path"];
            string password = ConfigurationManager.AppSettings["cert_password"];
            //X509Certificate cert = new X509Certificate(certpath, password);   
            X509Certificate2 cert = new X509Certificate2(certpath, password, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            //对服务端证书进行有效性校验（非第三方权威机构颁发的证书，如自己生成的，不进行验证，这里返回true）
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ClientCertificates.Add(cert);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";
            httpWebRequest.UseDefaultCredentials = true;

            byte[] btBodys = Encoding.UTF8.GetBytes(body);
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();
            return responseContent;
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="Number">字符串位数</param>
        /// <returns></returns>
        public static string randString(int Number)
        {
            string str = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"; 
            Random r = new Random();
            string result = string.Empty;

            for (int i = 0; i < Number; i++)
            {
                int m = r.Next(0, str.Length);
                string s = str.Substring(m, 1);
                result += s;
            }
            return result;
        }


        /// <summary>
        /// 输入一个整数数组，返回其中的一个随机整数
        /// </summary>
        /// <param name="intArray"></param>
        /// <returns></returns>
        public static int randInt()
        {
            int[] intArray = new int[] 
            {
                100,101,102,103,104,105,106,107,108,109,110,114,119,168,188
            };

            Random r = new Random();           
            int i = r.Next(0,intArray.Length-1);
            int result = intArray[i];
            return result;
        }

        /// <summary>
        /// CheckValidationResult的定义
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }



    }
}