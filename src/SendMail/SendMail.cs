using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace SendMail
{
    class SendMail
    {
        /// <summary>
        /// 发送邮件函数
        /// </summary>
        /// <param name="fromAddress">发送人地址</param>
        /// <param name="fromName">发送人名称</param>
        /// <param name="toAddress">收件人地址</param>
        /// <param name="toName">收件人名称</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="attachment">附件</param>
        /// <param name="smtpHost">smtp服务器</param>
        /// <param name="smtpUId">邮箱</param>
        /// <param name="smtpPwd">密码</param>
        /// <returns>密码</returns>
        public bool sendmail(string fromAddress, string fromName, string toAddress, string toName, string subject, string body, string attachment, string smtpHost, string smtpUId, string smtpPwd)
        {
            MailAddress from = new MailAddress(fromAddress, fromName);
            MailAddress to = new MailAddress(toAddress, toName);
            MailMessage mailMsg = new MailMessage(from, to);
            if (attachment != "")
            {
                mailMsg.Attachments.Add(new Attachment(attachment));
            }   
        
            mailMsg.Subject = subject; 
            mailMsg.Body = body;
            mailMsg.IsBodyHtml = false;
            mailMsg.BodyEncoding = System.Text.Encoding.UTF8;
            mailMsg.Priority = MailPriority.High;                               

            SmtpClient client = new SmtpClient();
            client.Host = smtpHost;
            client.Port = 25;
            client.Credentials = new NetworkCredential(smtpUId, smtpPwd);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                client.Send(mailMsg);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                mailMsg.Dispose();
            }
        }


    }
}
