using System;
namespace Weixin.Model
{
    /// <summary>
    /// 红包发放记录表
    /// </summary>
    [Serializable]
    public partial class WxOrder
    {
        public WxOrder()
        { }
        #region Model
        private int _wxorderid;
        private string _phoneno;
        private int? _totalnum = 3;
        private int? _remainnum = 3;
        private int? _totalamount;
        private DateTime? _lastgettime;
        private string _remark;
        private DateTime? _createtime = DateTime.Now;
        private string _createusername;
        private DateTime? _modifytime;
        private string _modifyusername;
        /// <summary>
        /// 主键Id
        /// </summary>
        public int WxOrderId
        {
            set { _wxorderid = value; }
            get { return _wxorderid; }
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
        /// 总次数
        /// </summary>
        public int? TotalNum
        {
            set { _totalnum = value; }
            get { return _totalnum; }
        }
        /// <summary>
        /// 剩余次数
        /// </summary>
        public int? RemainNum
        {
            set { _remainnum = value; }
            get { return _remainnum; }
        }
        /// <summary>
        /// 总金额
        /// </summary>
        public int? TotalAmount
        {
            set { _totalamount = value; }
            get { return _totalamount; }
        }
        /// <summary>
        /// 最后领取时间
        /// </summary>
        public DateTime? LastGetTime
        {
            set { _lastgettime = value; }
            get { return _lastgettime; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
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
        #endregion Model

    }
}

