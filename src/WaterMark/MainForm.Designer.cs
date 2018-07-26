namespace Luxlead.WaterMark
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelectOldPath = new System.Windows.Forms.Button();
            this.txtOldPath = new System.Windows.Forms.TextBox();
            this.btnAddWaterMark = new System.Windows.Forms.Button();
            this.txtWaterMark = new System.Windows.Forms.TextBox();
            this.lblOldPath = new System.Windows.Forms.Label();
            this.lblWaterMark = new System.Windows.Forms.Label();
            this.picNewImage = new System.Windows.Forms.PictureBox();
            this.lblNewPicture = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picNewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectOldPath
            // 
            this.btnSelectOldPath.Location = new System.Drawing.Point(512, 14);
            this.btnSelectOldPath.Name = "btnSelectOldPath";
            this.btnSelectOldPath.Size = new System.Drawing.Size(75, 23);
            this.btnSelectOldPath.TabIndex = 0;
            this.btnSelectOldPath.Text = "选择原图片";
            this.btnSelectOldPath.UseVisualStyleBackColor = true;
            this.btnSelectOldPath.Click += new System.EventHandler(this.btnSelectOldPath_Click);
            // 
            // txtOldPath
            // 
            this.txtOldPath.Location = new System.Drawing.Point(79, 14);
            this.txtOldPath.Name = "txtOldPath";
            this.txtOldPath.Size = new System.Drawing.Size(400, 21);
            this.txtOldPath.TabIndex = 1;
            // 
            // btnAddWaterMark
            // 
            this.btnAddWaterMark.Location = new System.Drawing.Point(512, 49);
            this.btnAddWaterMark.Name = "btnAddWaterMark";
            this.btnAddWaterMark.Size = new System.Drawing.Size(75, 23);
            this.btnAddWaterMark.TabIndex = 2;
            this.btnAddWaterMark.Text = "加文字水印";
            this.btnAddWaterMark.UseVisualStyleBackColor = true;
            this.btnAddWaterMark.Click += new System.EventHandler(this.btnAddWaterMark_Click);
            // 
            // txtWaterMark
            // 
            this.txtWaterMark.Location = new System.Drawing.Point(79, 49);
            this.txtWaterMark.Name = "txtWaterMark";
            this.txtWaterMark.Size = new System.Drawing.Size(400, 21);
            this.txtWaterMark.TabIndex = 3;
            this.txtWaterMark.Text = "LUXLEAD洛诗琳";
            // 
            // lblOldPath
            // 
            this.lblOldPath.AutoSize = true;
            this.lblOldPath.Location = new System.Drawing.Point(12, 17);
            this.lblOldPath.Name = "lblOldPath";
            this.lblOldPath.Size = new System.Drawing.Size(65, 12);
            this.lblOldPath.TabIndex = 4;
            this.lblOldPath.Text = "图片路径：";
            // 
            // lblWaterMark
            // 
            this.lblWaterMark.AutoSize = true;
            this.lblWaterMark.Location = new System.Drawing.Point(12, 52);
            this.lblWaterMark.Name = "lblWaterMark";
            this.lblWaterMark.Size = new System.Drawing.Size(65, 12);
            this.lblWaterMark.TabIndex = 5;
            this.lblWaterMark.Text = "水印文字：";
            // 
            // picNewImage
            // 
            this.picNewImage.Location = new System.Drawing.Point(14, 93);
            this.picNewImage.Name = "picNewImage";
            this.picNewImage.Size = new System.Drawing.Size(800, 600);
            this.picNewImage.TabIndex = 6;
            this.picNewImage.TabStop = false;
            // 
            // lblNewPicture
            // 
            this.lblNewPicture.Location = new System.Drawing.Point(323, 709);
            this.lblNewPicture.Name = "lblNewPicture";
            this.lblNewPicture.Size = new System.Drawing.Size(65, 12);
            this.lblNewPicture.TabIndex = 7;
            this.lblNewPicture.Text = "图片预览";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.lblNewPicture);
            this.Controls.Add(this.picNewImage);
            this.Controls.Add(this.lblWaterMark);
            this.Controls.Add(this.lblOldPath);
            this.Controls.Add(this.txtWaterMark);
            this.Controls.Add(this.btnAddWaterMark);
            this.Controls.Add(this.txtOldPath);
            this.Controls.Add(this.btnSelectOldPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "图片加水印";
            ((System.ComponentModel.ISupportInitialize)(this.picNewImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectOldPath;
        private System.Windows.Forms.TextBox txtOldPath;
        private System.Windows.Forms.Button btnAddWaterMark;
        private System.Windows.Forms.TextBox txtWaterMark;
        private System.Windows.Forms.Label lblOldPath;
        private System.Windows.Forms.Label lblWaterMark;
        private System.Windows.Forms.PictureBox picNewImage;
        private System.Windows.Forms.Label lblNewPicture;
    }
}

