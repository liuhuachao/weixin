using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weixin.Model
{
    ///
    /// 用户信息类
    ///
    public class OAuth_User
    {
        public OAuth_User() { }

        private string _openID;
        private string _searchText;
        private string _nickname;
        private string _sex;
        private string _province;
        private string _city;
        private string _country;
        private string _headimgUrl;
        private string _privilege;

        ///
        /// 用户的唯一标识
        ///
        public string openid
        {
            set { _openID = value; }
            get { return _openID; }
        }
        ///
        ///
        ///
        public string SearchText
        {
            set { _searchText = value; }
            get { return _searchText; }
        }
        ///
        /// 用户昵称
        ///
        public string nickname
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        ///
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        ///
        public string sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        ///
        /// 用户个人资料填写的省份
        ///
        public string province
        {
            set { _province = value; }
            get { return _province; }
        }
        ///
        /// 普通用户个人资料填写的城市
        ///
        public string city
        {
            set { _city = value; }
            get { return _city; }
        }
        ///
        /// 国家，如中国为CN
        ///
        public string country
        {
            set { _country = value; }
            get { return _country; }
        }
        ///
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        ///
        public string headimgurl
        {
            set { _headimgUrl = value; }
            get { return _headimgUrl; }
        }
        ///
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）其实这个格式称不上JSON，只是个单纯数组
        ///
        public string privilege
        {
            set { _privilege = value; }
            get { return _privilege; }
        }


    }
}