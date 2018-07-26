using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Weixin.DBUtility;
using Weixin.Code;
using System.Web.UI;

namespace Weixin.BLL
{
    public class PaymentRecord
    {
        private readonly Weixin.DAL.PaymentRecord dal = new Weixin.DAL.PaymentRecord();
        public PaymentRecord()
        { }

        /// <summary>
        /// 获取总领取次数
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <returns></returns>
        public int GetTotalNum(string phoneNo)
        {
            int intResult = 0;
            string sqlText = @"SELECT IFNULL(SUM(IFNULL(TotalNum,0)),0) FROM WxOrder ord WHERE ord.PhoneNo = @PhoneNo";
            MySqlParameter[] paraArray = new MySqlParameter[]
            { 
                new MySqlParameter("@PhoneNo",phoneNo),
            };
            object ojResult = DbHelperMySql.GetSingle(sqlText, paraArray);
            intResult = Convert.ToInt32(ojResult);
            return intResult;
        }
        /// <summary>
        /// 获取剩余领取次数
        /// </summary>
        /// <param name="phoneNo"></param>
        public int GetRemainNum(string phoneNo)
        {
            int intResult = 0;
            string sqlText = @"SELECT IFNULL(SUM(IFNULL(RemainNum,0)),0) FROM WxOrder ord WHERE ord.PhoneNo = @PhoneNo";
            MySqlParameter[] paraArray = new MySqlParameter[]
            { 
                new MySqlParameter("@PhoneNo",phoneNo),
            };
            object ojResult = DbHelperMySql.GetSingle(sqlText, paraArray);
            intResult = Convert.ToInt32(ojResult);
            return intResult;
        }
        /// <summary>
        /// 获取订单Id
        /// </summary>
        /// <param name="mch_billno"></param>
        /// <returns></returns>
        public string GetOrderId(string mch_billno)
        {
            string sqlText = @" SELECT IFNULL(WxOrderId,'') FROM WxOrder  WHERE Remark=@Remark ";
            MySqlParameter[] paraArray = new MySqlParameter[]
            { 
                new MySqlParameter("@Remark",mch_billno),
            };
            object ojResult = DbHelperMySql.GetSingle(sqlText, paraArray);
            return ojResult.ToString();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>返回值</returns>
        public int Add(Model.PaymentRecord model)
        {
            int intResult = 0;           
            MySqlParameter[] paraArray = new MySqlParameter[]
            { 
                new MySqlParameter("@PhoneNo",model.PhoneNo),
                new MySqlParameter("@nonce_str",model.nonce_str),
                new MySqlParameter("@sign",model.sign),
                new MySqlParameter("@mch_billno",model.mch_billno),
                new MySqlParameter("@wxappid",model.wxappid),
                new MySqlParameter("@send_name",model.send_name),
                new MySqlParameter("@re_openid",model.re_openid),
                new MySqlParameter("@total_amount",model.total_amount),
                new MySqlParameter("@total_num",model.total_num),
                new MySqlParameter("@wishing",model.wishing),
                new MySqlParameter("@client_ip",model.client_ip),
                new MySqlParameter("@act_name",model.act_name),
                new MySqlParameter("@remark",model.remark),
            };
            intResult = DbHelperMySql.Insert(model,"PaymentRecordId");
            return intResult;
        }

        /// <summary>
        /// 修改汇总表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(Model.WxOrder model)
        {
            int intResult = 0;
            string sqlText = @"
            UPDATE WxOrder wx1 
            INNER JOIN (SELECT * FROM WxOrder WHERE PhoneNo=@PhoneNo AND IFNULL(RemainNum,0)>0 ORDER BY WxOrderId limit 1 ) AS wx2 ON wx2.WxOrderId=wx1.WxOrderId 
            SET wx1.RemainNum=wx1.RemainNum-1,wx1.TotalAmount=IFNULL(wx1.TotalAmount,0)+@TotalAmount,wx1.LastGetTime=sysdate() 
            ,wx1.Remark=@Remark,wx1.ModifyTime=sysdate(),wx1.ModifyUserName='system' 
            WHERE wx1.PhoneNo=@PhoneNo AND IFNULL(wx1.RemainNum,0)>0
            ";
            MySqlParameter[] paraArray = new MySqlParameter[]
            { 
                new MySqlParameter("@PhoneNo",model.PhoneNo),
                new MySqlParameter("@TotalAmount",model.TotalAmount),
                new MySqlParameter("@Remark",model.Remark),
            };
            intResult = DbHelperMySql.ExecuteSql(sqlText, paraArray);
            return intResult;
        }


    }
}