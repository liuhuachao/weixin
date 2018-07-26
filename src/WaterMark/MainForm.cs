using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Luxlead.WaterMark
{
    public partial class MainForm : Form
    {
        string oldpath;
        string newpath;
        string content;
        Image image = null;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择原图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectOldPath_Click(object sender, EventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    oldpath = ofd.FileName;
            //    this.txtOldPath.Text = oldpath;
            //    image = Image.FromFile(oldpath);
            //    this.picNewImage.Image = image;
            //    newpath = Path.GetDirectoryName(oldpath)+"\\"+Path.GetFileNameWithoutExtension(oldpath)+"_new"+Path.GetExtension(oldpath);
            //}
            string userJson = @"{""openid"":""oLONWtwviRi7oNkvi33nnEj4pfg0"",""nickname"":""刘华超"",""sex"":1,""language"":""zh_CN"",""city"":""深圳"",""province"":""广东"",""country"":""中国"",""headimgurl"":""http:\/\/wx.qlogo.cn\/mmopen\/KlqSezvC8fiaGbLznVFXBE4CsETUbFLRxYAxObCXZ3WsahD8OLJCyIywgkHquxano0OibGnRQeKriczP1HOUNLGIg\/0"",""privilege"":[]}";
            OAuthUser oau = new OAuthUser();
            oau = JsonHelper.ParseFromJson<OAuthUser>(userJson);
            MessageBox.Show(oau.nickname);
            
        }

        /// <summary>
        /// 图片加文字水印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddWaterMark_Click(object sender, EventArgs e)
        {
            content = this.txtWaterMark.Text;
            if (string.IsNullOrEmpty(oldpath))
            {
                MessageBox.Show("请先选择原图片！");
                return;
            }
            if (string.IsNullOrEmpty(content))
            {
                MessageBox.Show("请先输入水印文字！");
                return; 
            }
            ImageWaterMark iwm = new ImageWaterMark();
            iwm.addWaterMark(oldpath, newpath, WaterMarkType.TextMark, WaterMarkPosition.LEFT_TOP, content); 
            Image image = Image.FromFile(newpath);
            this.picNewImage.Image = image;
        }



    }
}
