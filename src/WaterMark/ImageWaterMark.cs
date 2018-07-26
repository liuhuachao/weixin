using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Web;

namespace Luxlead.WaterMark
{
    //水印类型
    public enum WaterMarkType
    {
        TextMark,    //文字水印
        ImageMark    //图片水印
    };

    //水印位置
    public enum WaterMarkPosition
    {
        LEFT_TOP,    //左上角
        LEFT_BOTTOM, //左下角
        RIGHT_TOP,   //右上角
        RIGHT_BOTTOM //右下角
    };

    /// <summary>
    /// 图片加水印
    /// </summary>
    public class ImageWaterMark
    {
        public ImageWaterMark(){}

        /// <summary>
        /// 添加图片水印
        /// </summary>
        /// <param name="oldpath">原图片绝对地址</param>
        /// <param name="newpath">新图片放置的绝对地址</param>
        /// <param name="wmtType">要添加的水印的类型</param>
        /// <param name="wmPosition">水印位置</param>
        /// <param name="waterMarkContent">水印内容,若为文字水印，则是要添加的文字；若为图片水印，则是图片的路径</param>
        public void addWaterMark(string oldpath, string newpath,WaterMarkType wmType,WaterMarkPosition wmPosition,string wmContent)
        {
            try
            {
                Image image = Image.FromFile(oldpath);
                Bitmap b = new Bitmap(image.Width, image.Height);
                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.High;
                g.DrawImage(image, 0, 0, image.Width, image.Height);
                switch (wmType)
                {
                    case WaterMarkType.TextMark:
                        this.addWatermarkText(g, wmContent, wmPosition, image.Width, image.Height);
                        break;
                    case WaterMarkType.ImageMark:
                        Image waterMarkImage = Image.FromFile(wmContent);
                        this.addWatermarkImage(g, waterMarkImage, wmPosition, image.Width, image.Height);
                    break;
                }
                b.Save(newpath,ImageFormat.Jpeg);
                b.Dispose();
                image.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show("图片生成水印异常："+ex.Message);                
            }
            finally
            {

            }  
        }

        /// <summary>
        /// 加水印文字
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="_watermarkText">水印文字内容</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        private void addWatermarkText(Graphics picture, string _watermarkText,WaterMarkPosition _waterMarkPosition, int _width, int _height)
        {
            // 确定水印文字的字体大小
            int[] sizes = new int[]{32, 30, 28, 26, 24, 22, 20, 18, 16, 14, 12, 10, 8, 6, 4};
            Font crFont = null;
            SizeF crSize = new SizeF();
            for (int i = 0;i < sizes.Length; i++)
            {
                crFont = new Font("Arial Black", sizes[i], FontStyle.Bold);
                crSize = picture.MeasureString(_watermarkText, crFont);
                if((ushort)crSize.Width < (ushort)_width)
                {
                    break;
                }
            }
            // 生成水印图片（将文字写到图片中）
            Bitmap floatBmp = new Bitmap((int)crSize.Width + 3, (int)crSize.Height + 3, PixelFormat.Format32bppArgb);
            Graphics fg=Graphics.FromImage(floatBmp);
            PointF pt=new PointF(0,0);
            // 画阴影文字
            Brush TransparentBrush0 = new SolidBrush(Color.FromArgb(255, Color.Black));
            Brush TransparentBrush1 = new SolidBrush(Color.FromArgb(255, Color.Black));
            fg.DrawString(_watermarkText,crFont,TransparentBrush0, pt.X, pt.Y + 1); 
            fg.DrawString(_watermarkText,crFont,TransparentBrush0, pt.X + 1, pt.Y); 
            fg.DrawString(_watermarkText,crFont,TransparentBrush1, pt.X + 1, pt.Y + 1); 
            fg.DrawString(_watermarkText,crFont,TransparentBrush1, pt.X, pt.Y + 2); 
            fg.DrawString(_watermarkText,crFont,TransparentBrush1, pt.X + 2, pt.Y); 
            TransparentBrush0.Dispose(); 
            TransparentBrush1.Dispose();
            // 画文字
            fg.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.HighQuality; 
            fg.DrawString(_watermarkText, crFont, new SolidBrush(Color.White), pt.X, pt.Y, StringFormat.GenericDefault);
            // 保存刚才的操作
            fg.Save(); 
            fg.Dispose();
            // 将水印图片加到原图中
            this.addWatermarkImage(picture, new Bitmap(floatBmp),_waterMarkPosition, _width, _height);
        }

        /// <summary>
        ///  加水印图片
        /// </summary>
        /// <param name="picture">Image对象</param>
        /// <param name="iTheImage">Image对象（以此图片为水印）</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        private void addWatermarkImage(Graphics picture, Image iTheImage, WaterMarkPosition _waterMarkPosition, int _width, int _height)
        {
            Image watermark = new Bitmap(iTheImage);
            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = {colorMap};
            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
            float[][] colorMatrixElements =
            {
                new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                new float[] {0.0f, 0.0f, 0.0f, 0.3f, 0.0f},
                new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
            };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            int xpos = 0;
            int ypos = 0;
            int WatermarkWidth = 0;
            int WatermarkHeight = 0;
            double bl = 1d;
            //计算水印图片的比率
            //取背景的1/4宽度来比较
            if ((_width > watermark.Width * 4) && (_height > watermark.Height * 4))
            {
                bl = 1;
            }
            else if ((_width > watermark.Width * 4) && (_height<watermark.Height * 4))
            {
                bl = Convert.ToDouble(_height / 4) / Convert.ToDouble(watermark.Height);
            
            }
            else if ((_width < watermark.Width * 4) && (_height > watermark.Height * 4))
            {
                bl = Convert.ToDouble(_width / 4) / Convert.ToDouble(watermark.Width);
            }
            else
            {
                if ((_width * watermark.Height) > (_height * watermark.Width))
                {
                    bl = Convert.ToDouble(_height / 4) / Convert.ToDouble(watermark.Height);
                    
                }
                else
                {
                    bl = Convert.ToDouble(_width / 4) / Convert.ToDouble(watermark.Width);
                    
                }
            
            }
            WatermarkWidth = Convert.ToInt32(watermark.Width * bl);
            WatermarkHeight = Convert.ToInt32(watermark.Height * bl);
            switch (_waterMarkPosition)
            {
                case WaterMarkPosition.LEFT_TOP:
                    xpos = 10;
                    ypos = 10;
                    break;
                case WaterMarkPosition.RIGHT_TOP:
                    xpos = _width - WatermarkWidth - 10;
                    ypos = 10;
                    break;
                case WaterMarkPosition.RIGHT_BOTTOM:
                    xpos = _width - WatermarkWidth - 10;
                    ypos = _height -WatermarkHeight - 10;
                    break;
                case WaterMarkPosition.LEFT_BOTTOM:
                    xpos = 10;
                    ypos = _height - WatermarkHeight - 10;
                    break;
            }
            picture.DrawImage( watermark, new Rectangle(xpos, ypos, WatermarkWidth, WatermarkHeight), 0, 0, watermark.Width, watermark.Height,GraphicsUnit.Pixel, imageAttributes);
            watermark.Dispose();
            imageAttributes.Dispose();
        }



    }
}
