using System;

namespace Weixin.Model
{
    /// <summary>
    /// 红包发放记录表
    /// </summary>
    [Serializable]
    public partial class PaymentRecord
    {
        public PaymentRecord(){ }

        private int _paymentrecordid;
        private string _phoneno;
        private string _wxorderId;
        private string _nonce_str;
        private string _sign;
        private string _mch_billno;
        private string _mch_id;
        private string _wxappid;
        private string _send_name;
        private string _re_openid;
        private int? _total_amount;
        private int? _total_num;
        private string _wishing;
        private string _client_ip;
        private string _act_name;
        private string _remark;
        private string _return_code;
        private string _return_msg;
        private string _err_code;
        private string _err_code_des;
        private DateTime? _send_time;
        private string _send_listid;
        private DateTime? _createtime = DateTime.Now;
        private string _createusername;
        private DateTime? _modifytime;
        private string _modifyusername;
        /// <summary>
        /// 主键Id
        /// </summary>
        public int PaymentRecordId
        {
            set { _paymentrecordid = value; }
            get { return _paymentrecordid; }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNo
        {
            set { _phoneno = value; }
            get { return _phoneno; }
        }
        /// <summary>
        /// 订单Id
        /// </summary>
        public string WxOrderId
        {
            set { _wxorderId = value; }
            get { return _wxorderId; }
        }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str
        {
            set { _nonce_str = value; }
            get { return _nonce_str; }
        }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign
        {
            set { _sign = value; }
            get { return _sign; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string mch_billno
        {
            set { _mch_billno = value; }
            get { return _mch_billno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string mch_id
        {
            set { _mch_id = value; }
            get { return _mch_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string wxappid
        {
            set { _wxappid = value; }
            get { return _wxappid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string send_name
        {
            set { _send_name = value; }
            get { return _send_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string re_openid
        {
            set { _re_openid = value; }
            get { return _re_openid; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public int? total_amount
        {
            set { _total_amount = value; }
            get { return _total_amount; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int? total_num
        {
            set { _total_num = value; }
            get { return _total_num; }
        }
        /// <summary>
        /// 祝福语
        /// </summary>
        public string wishing
        {
            set { _wishing = value; }
            get { return _wishing; }
        }
        /// <summary>
        /// Ip地址
        /// </summary>
        public string client_ip
        {
            set { _client_ip = value; }
            get { return _client_ip; }
        }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string act_name
        {
            set { _act_name = value; }
            get { return _act_name; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 返回状态码
        /// </summary>
        public string return_code
        {
            set { _return_code = value; }
            get { return _return_code; }
        }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string return_msg
        {
            set { _return_msg = value; }
            get { return _return_msg; }
        }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code
        {
            set { _err_code = value; }
            get { return _err_code; }
        }
        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des
        {
            set { _err_code_des = value; }
            get { return _err_code_des; }
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? send_time
        {
            set { _send_time = value; }
            get { return _send_time; }
        }
        /// <summary>
        /// 微信单号
        /// </summary>
        public string send_listid
        {
            set { _send_listid = value; }
            get { return _send_listid; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreateUserName
        {
            set { _createusername = value; }
            get { return _createusername; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime
        {
            set { _modifytime = value; }
            get { return _modifytime; }
        }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string ModifyUserName
        {
            set { _modifyusername = value; }
            get { return _modifyusername; }
        }


    }
}

