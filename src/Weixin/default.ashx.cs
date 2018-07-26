using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace Weixin
{
    /// <summary>
    /// 微信接口。统一接收并处理信息的入口。
    /// </summary>
    public class _default : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string postString = string.Empty;
            string echoString = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];         

            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                using (Stream stream = HttpContext.Current.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                }

                if (!string.IsNullOrEmpty(postString))
                {
                    Handle(postString);
                }
            }
            else
            {
                Auth();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 处理各种请求信息并应答（通过POST的请求）
        /// </summary>
        /// <param name="postStr">POST方式提交的数据</param>
        //private void Execute(string postStr)
        //{
        //    WeixinApiDispatch dispatch = new WeixinApiDispatch();
        //    string responseContent = dispatch.Execute(postStr);

        //    HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
        //    HttpContext.Current.Response.Write(responseContent);
        //}

        /// <summary>
        /// 处理信息并应答
        /// </summary>
        private void Handle(string postStr)
        {

        }

        /// <summary>
        /// 成为开发者的第一步，验证并处理服务器的数据
        /// </summary>
        private void Auth()
        {
            string token = ConfigurationManager.AppSettings["WeixinToken"];
            if (string.IsNullOrEmpty(token))
            {
                Debug.WriteLine(string.Format("WeixinToken 配置项没有配置！"));
            }
            string echoString = HttpContext.Current.Request.QueryString["echoStr"];
            string signature = HttpContext.Current.Request.QueryString["signature"];
            string timestamp = HttpContext.Current.Request.QueryString["timestamp"];
            string nonce = HttpContext.Current.Request.QueryString["nonce"];

            if (new Weixin._default().CheckSignature(token, signature, timestamp, nonce))
            {
                if (!string.IsNullOrEmpty(echoString))
                {
                    HttpContext.Current.Response.Write(echoString);
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        public bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = sha1(tmpStr);
            tmpStr = tmpStr.ToLower();

            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// sha1加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string sha1(string text)
        {
            byte[] cleanBytes = Encoding.Default.GetBytes(text);
            byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }      


    }
}