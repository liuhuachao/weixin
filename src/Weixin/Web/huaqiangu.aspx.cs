using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weixin.Code;
using Weixin.Model;

namespace Weixin.Web
{
    public partial class huaqiangu : System.Web.UI.Page
    {
        LogHelper log = new LogHelper("花千骨页面日志");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = HttpContext.Current.Request.QueryString["code"];
                string appid = ConfigurationManager.AppSettings["AppID"];
                if (string.IsNullOrEmpty(code))
                {
                    log.WriteLog(string.Format("正在获取code"));
                    Response.Redirect(string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http%3a%2f%2fweixin.luxlead.com%2fWeb%2fhuaqiangu.aspx&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect ", appid));
                    return;
                }
                else
                {
                    OAuth_Token ot = new OAuth_Token();
                    OAuth_User userinfo = new OAuth_User();
                    string json = weixin.GetUserInfo(new string[] { code }, "GetAccessToken");
                    ot = JsonHelper.ParseFromJson<OAuth_Token>(json);
                    if (string.IsNullOrEmpty(ot.access_token))
                    {
                        log.WriteLog(string.Format("正在获取access_token"));
                        Response.Redirect(string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http%3a%2f%2fweixin.luxlead.com%2fWeb%2fhuaqiangu.aspx&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect ", appid));
                        return;
                    }
                    else
                    {
                        try
                        {
                            json = weixin.GetUserInfo(new string[] { ot.access_token, ot.openid }, "GetUserInfo");
                            userinfo = JsonHelper.ParseFromJson<OAuth_User>(json);
                            Label1.Text = userinfo.nickname;
                            this.avt.Src = userinfo.headimgurl;
                            log.WriteLog(string.Format("获取到用户信息成功：{0}", json));
                        }
                        catch (Exception ex)
                        {
                            log.WriteLog(string.Format("获取到用户信息失败，错误信息：{0}", ex.Message));
                        }
                    }
                }
            }


        }
    }
}