using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SendMail
{
    public partial class frmSendMail : Form
    {
        public frmSendMail()
        {
            InitializeComponent();
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            string from = "liuhuachao@163.com";
            string fromer = "发件人";
            string to = "liuhuachao@luxlead.com";
            string toer = "收件人";
            string subject = "邮件标题";
            string file = "";
            string body = "HelloWorld";
            string SMTPHost = "smtp.163.com";
            string SMTPuser = "liuhuachao@163.com";
            string SMTPpass = "lhj198163";
            SendMail sm = new SendMail();
            sm.sendmail(from, fromer, to, toer, subject, body, file, SMTPHost, SMTPuser, SMTPpass);
        }


    }
}
