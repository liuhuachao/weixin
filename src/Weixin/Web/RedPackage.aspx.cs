using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using MySql.Data.MySqlClient;
using System.Web.Configuration;
using System.Globalization;
using Weixin.Code;
using Weixin.DBUtility;
using Weixin.Model;

namespace Weixin.Web
{
    public partial class RedPackage : System.Web.UI.Page
    {
        LogHelper log = new LogHelper("微信红包页面日志");
        BLL.PaymentRecord bllPR = new BLL.PaymentRecord();   
  
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取用户标识openId
            if (!IsPostBack)
            {
                string code = HttpContext.Current.Request.QueryString["code"];
                string appid = ConfigurationManager.AppSettings["AppID"];
                if (string.IsNullOrEmpty(code))
                {
                    Response.Redirect(string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http%3a%2f%2fweixin.luxlead.com%2fWeb%2fRedPackage.aspx&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect ", appid));
                    return;
                }
                else
                {
                    OAuth_Token ot = new OAuth_Token();
                    string json = weixin.GetUserInfo(new string[] { code }, "GetAccessToken");
                    ot = JsonHelper.ParseFromJson<OAuth_Token>(json);

                    if (string.IsNullOrEmpty(ot.access_token))
                    {
                        log.WriteLog("获取到用户标识openId失败，无法获取到access_token，正在重定向!");
                        Response.Redirect(string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http%3a%2f%2fweixin.luxlead.com%2fWeb%2fRedPackage.aspx&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect ", appid));
                        return;
                    }
                    else
                    {
                        ViewState["isSucess"] = true;
                        ViewState["re_openid"] = ot.openid;
                        log.WriteLog(string.Format("获取到用户标识成功:openId：{0},access_token:{1}", ot.openid,ot.access_token));
                    }
                }
            }
        }

        /// <summary>
        /// 点击领取按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetRedPackage_Click(object sender, EventArgs e)
        {
            //请在0到8点之外的时间领取
            int hour = DateTime.Now.Hour;
            if (hour >= 0 && hour < 8)
            {
                this.lblMsg.Text = "很抱歉，请在0：00-8：00之外的时间领红包。";
                log.WriteLog("很抱歉，请在0：00-8：00之外的时间领红包。");
                return;
            }

            //获取到用户标识
            bool isSucess = false;
            string re_openid = "";
            if (ViewState["isSucess"] != null && ViewState["re_openid"] != null)
            {
                isSucess = (bool)ViewState["isSucess"];
                re_openid = ViewState["re_openid"].ToString();
                if (!isSucess || string.IsNullOrEmpty(re_openid))
                {
                    this.lblMsg.Text = "很抱歉,获取用户信息失败，请联系客服！";
                    log.WriteLog("很抱歉,获取用户信息失败，请联系客服！");
                    return;
                }
            }
            else
            {
                this.lblMsg.Text = "很抱歉,获取用户信息失败，请联系客服！";
                log.WriteLog("很抱歉,获取用户信息失败，请联系客服！");
                return; 
            }
                
            //判断手机号是否合法
            string phoneNo = this.txtPhoneNo.Text;                      //手机号   
            string msg = "";
            if (!CheckIsLegal(phoneNo,out msg))                         //验证输入是否合法
            {                
                this.lblMsg.Text = msg;
                return;
            }    
        

            string nonce_str = weixin.randString(32);                   //随机字符串            
            string appid = ConfigurationManager.AppSettings["AppID"];   //公众账号
            string mch_id = ConfigurationManager.AppSettings["mch_id"]; //商户号
            string mch_billno = mch_id + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Ticks.ToString().PadLeft(18).Substring(8, 10);  //商户订单号
            string send_name = "Luxlead洛诗琳";                        //商户名称
            string act_name = "双11红包疯狂送";                        //活动名称
            string wishing = "恭喜发财,大吉大利！";                    //祝福语
            string client_ip = "180.76.132.217";                       //IP地址
            string remark = "红包备注";                                //备注
            int total_amount = weixin.randInt();                       //总金额(单位为分)
            int total_num = 1;                                         //总数量(单位为个)

            //参数字符串
            string parm = string.Format("nonce_str={0}&mch_billno={1}&mch_id={2}&wxappid={3}&send_name={4}&re_openid={5}&total_amount={6}&total_num={7}&wishing={8}&client_ip={9}&act_name={10}&remark={11}", nonce_str, mch_billno, mch_id, appid, send_name, re_openid, total_amount,total_num,wishing,client_ip, act_name,remark);
            //生成参数签名
            string[] parmarr = parm.Split('&');
            Dictionary<string, string> di = new Dictionary<string, string>();
            foreach (string item in parmarr)
            {
                string[] dd = item.Split('=');
                di.Add(dd[0], dd[1]);
            }
            string sign = Sign.GetSign(di);
            parm = parm + "&sign=" + sign;            

            string body = string.Format(@"<xml>
            <sign><![CDATA[{0}]]></sign> 
            <mch_billno><![CDATA[{1}]]></mch_billno> 
            <mch_id><![CDATA[{2}]]></mch_id> 
            <wxappid><![CDATA[{3}]]></wxappid> 
            <send_name><![CDATA[{4}]]></send_name> 
            <re_openid><![CDATA[{5}]]></re_openid> 
            <total_amount><![CDATA[{6}]]></total_amount> 
            <total_num><![CDATA[{7}]]></total_num> 
            <wishing><![CDATA[{8}]]></wishing> 
            <client_ip><![CDATA[{9}]]></client_ip> 
            <act_name><![CDATA[{10}]]></act_name> 
            <remark><![CDATA[{11}]]></remark> 
            <nonce_str><![CDATA[{12}]]></nonce_str> 
            </xml>", sign, di["mch_billno"], di["mch_id"], di["wxappid"], di["send_name"], di["re_openid"], di["total_amount"], di["total_num"], di["wishing"], di["client_ip"], di["act_name"], di["remark"], di["nonce_str"]);
            log.WriteLog("微信红包接口Post的body数据是：" + body);            
            try
            {
                string returnXml = weixin.PostHttp("https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack", body);
                log.WriteLog("微信红包接口返回字符串：" + returnXml);

                //解析返回数据,跳转到领取页面
                string returnMsg = "",showMsg="";
                XmlSerialize xmlS = new XmlSerialize();
                BaseReturn baseReturn = xmlS.XmlToModel<BaseReturn>(returnXml, "BaseReturn");
                if (baseReturn.return_code.ToUpper() == "SUCCESS")
                {
                    SuccessReturn successReturn = xmlS.XmlToModel<SuccessReturn>(returnXml, "SuccessReturn");
                    if (successReturn.result_code.ToUpper() == "SUCCESS")
                    {
                        //将红包领取汇总记录写入数据库
                        Model.WxOrder modelOrder = new WxOrder()
                        {
                            PhoneNo = phoneNo,
                            TotalAmount = total_amount,
                            Remark = mch_billno,
                        };
                        if (bllPR.Update(modelOrder) <= 0)
                        {
                            log.WriteLog("微信红包领取汇总记录保存到数据库失败！");
                            this.lblMsg.Text = "很抱歉，领取失败，系统错误请联系客服！";
                            return;
                        }
                        else
                        {
                            log.WriteLog("微信红包领取汇总记录保存到数据库成功！");
                        }

                        //将红包领取明细记录写入数据库                        
                        Model.PaymentRecord modlePR = new Model.PaymentRecord()
                        {
                            PhoneNo = phoneNo,
                            WxOrderId = bllPR.GetOrderId(mch_billno),
                            nonce_str = nonce_str,
                            sign = sign,
                            mch_billno = mch_billno,
                            mch_id = mch_id,
                            wxappid = appid,
                            send_name = send_name,
                            re_openid = re_openid,
                            total_amount = total_amount,
                            total_num = total_num,
                            wishing = wishing,
                            client_ip = client_ip,
                            act_name = act_name,
                            remark = remark,
                            return_code = successReturn.result_code,
                            return_msg = successReturn.return_msg,
                            err_code = successReturn.err_code,
                            err_code_des = successReturn.err_code_des,
                            send_time = DateTime.ParseExact(successReturn.send_time, "yyyyMMddHHmmss", CultureInfo.CurrentCulture),
                            send_listid = successReturn.send_listid,
                        };
                        if (bllPR.Add(modlePR) <= 0)
                        {
                            log.WriteLog("微信红包领取明细记录保存到数据库失败！");
                            this.lblMsg.Text = "很抱歉，领取失败，请稍候再试！";
                            return;
                        }
                        else
                        {
                            log.WriteLog("微信红包领取明细记录保存到数据库成功！");
                        }

                        returnMsg = string.Format("微信红包接口发送红包成功,发送时间是{0},微信订单号是{1}", successReturn.send_time, successReturn.send_listid);
                        log.WriteLog(returnMsg);
                        showMsg = "恭喜你，领取成功！";
                        this.lblMsg.Text = showMsg;
                        this.btnGetRedPackage.Text = "领取成功!";
                        
                        Response.Redirect("GetSuccess.aspx",false);
                        return;
                    }
                    else
                    {                        
                        ErrorReturn errorReturn = xmlS.XmlToModel<ErrorReturn>(returnXml, "ErrorReturn");                        
                        returnMsg = string.Format("微信红包接口发送红包失败，错误代号:{0},错误消息：{1}。", errorReturn.err_code, errorReturn.err_code_des);
                        switch (errorReturn.err_code.ToUpper())
                        {
                            case "NO_AUTH":
                                showMsg = string.Format("很抱歉，该微信账号异常，请使用活跃的微信号。");
                                break;
                            case "TIME_LIMITED":
                                showMsg = string.Format("很抱歉，请在0：00-8：00之外的时间领红包。");
                                break;
                            case "NOTENOUGH":
                                showMsg = string.Format("很抱歉，账户余额不足，请联系客服处理。");
                                break;
                            default:
                                showMsg = "很抱歉，领取失败,请联系客服处理。";
                                break;
                        }
                    }
                }
                else
                {
                    ErrorReturn errorReturn = xmlS.XmlToModel<ErrorReturn>(returnXml,"ErrorReturn");
                    returnMsg = string.Format("微信红包接口发送红包失败，错误代号:{0},错误消息：{1}。", errorReturn.err_code, errorReturn.err_code_des);
                    switch (errorReturn.err_code.ToUpper())
                    {
                        case "NO_AUTH":
                            showMsg = string.Format("很抱歉，该微信账号异常，请使用活跃的微信号。");
                            break;
                        case "TIME_LIMITED":
                            showMsg = string.Format("很抱歉，请在0：00-8：00之外的时间领红包。");
                            break;
                        case "NOTENOUGH":
                            showMsg = string.Format("很抱歉，账户余额不足，请联系客服处理。");
                            break;
                        default:
                            showMsg = "很抱歉，领取失败,请联系客服处理。";
                            break;
                    }                 
                }
                if (!string.IsNullOrEmpty(showMsg))
                {
                    this.btnGetRedPackage.Text = "领红包";
                }
                log.WriteLog(returnMsg);
                this.lblMsg.Text = showMsg;
            }
            catch (Exception ex)
            {
                log.WriteLog("微信红包接口调用错误,错误信息：" + ex.Message);
            }          
        }

        /// <summary>
        /// 检查领取是否合法
        /// </summary>
        /// <param name="phoneNo">手机号</param>
        private bool CheckIsLegal(string phoneNo,out string msg)
        {
            bool isLegal = false;
            string strMsg = "",showMsg = "";
            if (string.IsNullOrEmpty(phoneNo))
            {
                isLegal = false;
                strMsg = "手机号不能为空,请输入手机号！";
                showMsg = strMsg;
            }
            else
            {
                if (!StringValidate.IsMobilePhone(phoneNo))
                {
                    strMsg = "请输入合法的手机号码!";
                    showMsg = strMsg;
                }
                else
                {
                    int totalNum = 0;
                    int remainNum = 0;
                    try
                    {
                        totalNum = bllPR.GetTotalNum(phoneNo);
                        remainNum = bllPR.GetRemainNum(phoneNo);
                    }
                    catch (MySqlException ex)
                    {
                        strMsg = string.Format("数据库获取红包可领取总次数和剩余次数出错，错误信息:{0}", ex.Message);
                        showMsg = string.Format("很抱歉，领取失败！系统错误请联系客服", ex.Message); ;
                    }
                    if (totalNum <= 0)
                    {
                        strMsg = string.Format("该手机号{0}没有领取资格，订单未导入或手机号错误！", phoneNo);
                        showMsg = strMsg;
                    }
                    else
                    {
                        if (remainNum <= 0)
                        {
                            strMsg = string.Format("该手机号{0}总共有{1}次领取资格，现在全部已领完！", phoneNo, totalNum);
                            showMsg = strMsg;
                        }
                        else
                        {
                            isLegal = true;
                            strMsg = string.Format("该手机号{0}总共有{1}次领取资格,还能领取{2}次！", phoneNo, totalNum, remainNum);
                            showMsg = strMsg;
                        }
                    }  
                }                      
            }
            log.WriteLog(strMsg);
            msg = showMsg;
            return isLegal; 
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