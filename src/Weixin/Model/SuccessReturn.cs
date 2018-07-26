using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weixin.Model
{
    public class SuccessReturn
    {
        public SuccessReturn() { }

        private string _return_code;             
        private string _return_msg;
        private string _result_code;   
        private string _sign;
        private string _err_code;
        private string _err_code_des;
        private string _mch_billno;
        private string _mch_id;
        private string _wxappid;
        private string _re_openid;
        private string _send_time;
        private string _send_listid;

        ///
        /// 返回代号
        ///
        public string return_code
        {
            set { _return_code = value; }
            get { return _return_code; }
        }
        ///
        /// 返回消息
        ///
        public string return_msg
        {
            set { _return_msg = value; }
            get { return _return_msg; }
        }
        ///
        /// 业务结果
        ///
        public string result_code
        {
            set { _result_code = value; }
            get { return _result_code; }
        }
        ///
        /// 签名
        ///
        public string sign
        {
            set { _sign = value; }
            get { return _sign; }
        }
        ///
        /// 错误代号
        ///
        public string err_code
        {
            set { _err_code = value; }
            get { return _err_code; }
        }
        ///
        /// 错误消息
        ///
        public string err_code_des
        {
            set { _err_code_des = value; }
            get { return _err_code_des; }
        }
        ///
        /// 商户订单号
        ///
        public string mch_billno
        {
            set { _mch_billno = value; }
            get { return _mch_billno; }
        }
        ///
        /// 商户号
        ///
        public string mch_id
        {
            set { _mch_id = value; }
            get { return _mch_id; }
        }
        ///
        /// 公众号
        ///
        public string wxappid
        {
            set { _wxappid = value; }
            get { return _wxappid; }
        }
        ///
        /// 用户标识
        ///
        public string re_openid
        {
            set { _re_openid = value; }
            get { return _re_openid; }
        }
        ///
        /// 发送时间
        ///
        public string send_time
        {
            set { _send_time = value; }
            get { return _send_time; }
        }
        ///
        /// 微信单号
        ///
        public string send_listid
        {
            set { _send_listid = value; }
            get { return _send_listid; }
        }



    }
}