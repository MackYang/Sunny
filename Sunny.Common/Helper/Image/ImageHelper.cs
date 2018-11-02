using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Sunny.Common.Helper
{
    /// <summary>
    /// 图像辅助类
    /// </summary>
    public class ImageHelper
    {


        #region 图片与base64字符串的转换,暂时由SerializeHelper中的序列化和反序列化方法替代

        /// <summary>  
        /// 图像转换为Base64编码  
        /// </summary>  
        /// <param name="image">图像</param>  
        /// <param name="format">图像格式</param>  
        /// <param name="throwException">出现异常时是否抛出</param>
        /// <returns>转换成功返回其Base64编码；失败返回空串</returns>  
        public static string ImageToBase64(Image image, ImageFormat format)
        {

            string base64String = "";
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, format);
                    byte[] imageBytes = ms.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将图片转成base64字符串时出现异常:" + ex);
            }

            return base64String;

        }

        /// <summary>  
        /// Base64编码转换为图像  
        /// </summary>  
        /// <param name="base64String">Base64字符串</param>  
        /// <param name="throwException">出现异常时是否抛出</param>
        /// <returns>转换成功返回图像；失败返回null</returns>  
        public static Image Base64ToImage(string base64String)
        {

            MemoryStream ms = null;
            Image image = null;
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                image = Image.FromStream(ms, true);
                ms.Flush();
                ms.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("将base64字符串转成图片时出现异常:" + ex);
            }
            return image;

        }


        #endregion

        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="sourceFile">源图片完整路径</param>
        /// <param name="destFile">缩略图存放目录</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="throwException">出现异常时是否抛出</param>
        /// <returns>创建成功返回True</returns>
        public static bool CreateThumbnail(string sourceFile, string destFile, int width, int height)
        {

            if (string.IsNullOrWhiteSpace(sourceFile) || string.IsNullOrWhiteSpace(destFile))
            {
                throw new Exception("生成缩略图时，源路径或目标不路径不能为空!");

            }
            if (width <= 0 || height <= 0)
            {
                throw new Exception("缩略图的宽度或高度不正确,请检查!");

            }
            if (!File.Exists(sourceFile))
            {
                throw new Exception("生成缩略图时原文件不存在!");

            }
            if (!IsImage(sourceFile))
            {
                throw new Exception("生成缩略图时原文件格式不正确!");
            }

            string extensionName = Path.GetExtension(sourceFile).ToLower();
            try
            {
                switch (extensionName)
                {
                    case ".png": return PngThumbnail(sourceFile, destFile, width, height);
                    default:
                        return GenThumbnail(sourceFile, destFile, width, height);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("生成缩略图时发生异常：" + ex);
            }


        }

        /// <summary>
        /// 生成PNG缩略图
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destFile"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static bool PngThumbnail(string sourceFile, string destFile, int width, int height)
        {
            System.Drawing.Image.GetThumbnailImageAbort myCallBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallBack);//提供一个回调方法，用于决定获取缩略图的方法在何时提前取消执行。

            Bitmap bmp = null;
            System.Drawing.Image thumbnail = null;
            try
            {
                bmp = new Bitmap(sourceFile);
                if (bmp.Width > bmp.Height)//计算等比例缩放的宽高，原图中2者中较大的值来计算
                {
                    height = bmp.Height * width / bmp.Width;
                }
                else
                {
                    width = bmp.Width * height / bmp.Height;
                }

                thumbnail = bmp.GetThumbnailImage(width, height, myCallBack, IntPtr.Zero);//检索图片中是否包含缩略图，如果不包含，就按比例缩放来生成一个缩略图。
                string destPath = Path.GetDirectoryName(destFile);
                if (!string.IsNullOrWhiteSpace(destPath))
                {
                    if (!Directory.Exists(destPath))
                    {
                        Directory.CreateDirectory(destPath);
                    }
                }
                thumbnail.Save(destFile);//将图像保存到指定path中。
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("生成PNG缩略图时发生异常：" + ex);
            }
            finally
            {
                bmp.DisposeIfNotNull();
                thumbnail.DisposeIfNotNull();
            }
        }

        private static bool ThumbnailCallBack()//CreateThumbnail方法需要调用这个方法。
        {
            return false;
        }
        /// <summary>
        /// 判断一个文件名是否是图片格式
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>true表示是</returns>
        public static bool IsImage(string fileName)
        {
            string exName = fileName.Substring(fileName.LastIndexOf('.'));
            string format = ".gif.jpg.jpeg.png.bmp.ico.tif.tiff.wbmp";
            return format.Contains(exName.ToLower());
        }

        /// <summary>
        /// 根据扩展名获取图片格式类型
        /// </summary>
        /// <param name="exName">扩展名</param>
        /// <returns></returns>
        public static ImageFormat GetFormat(string exName)
        {
            if (!string.IsNullOrWhiteSpace(exName))
            {
                exName = exName.ToLower();
                switch (exName)
                {
                    case ".jpg":
                        return ImageFormat.Jpeg;
                    case ".jpeg":
                        return ImageFormat.Jpeg;
                    case ".gif":
                        return ImageFormat.Gif;
                    case ".png":
                        return ImageFormat.Png;
                    case ".bmp":
                        return ImageFormat.Bmp;
                    case ".ico":
                        return ImageFormat.Icon;
                    case ".icon":
                        return ImageFormat.Icon;
                    case ".tif":
                        return ImageFormat.Tiff;
                    case ".tiff":
                        return ImageFormat.Tiff;
                    default:
                        return ImageFormat.Jpeg;
                }
            }

            return null;
        }

        /// <summary> 
        ///  生成普通缩略图
        /// </summary> 
        /// <param name="sourceFile"> 源图的路径(含文件名及扩展名) </param> 
        /// <param name="destFile"> 生成的缩略图所保存的路径(含文件名及扩展名)</param> 
        /// <param name="width"> 欲生成的缩略图的宽度(像素值) </param> 
        /// <param name="height"> 欲生成的缩略图的高度(像素值) </param> 
        private static bool GenThumbnail(string sourceFile, string destFile, int width, int height)
        {
            System.Drawing.Image imageFrom = null;
            Bitmap bmp = null;
            Graphics g = null;
            try
            {
                imageFrom = System.Drawing.Image.FromFile(sourceFile);

                // 源图宽度及高度 
                int imageFromWidth = imageFrom.Width;
                int imageFromHeight = imageFrom.Height;

                // 根据源图及欲生成的缩略图尺寸,计算缩略图的实际尺寸及其在"画布"上的位置 
                if (imageFromWidth > imageFromHeight)
                {
                    height = imageFromHeight * width / imageFromWidth;
                }
                else
                {
                    width = imageFromWidth * height / imageFromHeight;
                }
                // 创建画布 
                bmp = new Bitmap(width, height);
                g = Graphics.FromImage(bmp);
                // 用白色清空 
                g.Clear(Color.White);
                // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // 指定高质量、低速度呈现。 
                g.SmoothingMode = SmoothingMode.HighQuality;
                // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。 
                g.DrawImage(imageFrom, new Rectangle(0, 0, width, height), new Rectangle(0, 0, imageFromWidth, imageFromHeight), GraphicsUnit.Pixel);

                string destPath = Path.GetDirectoryName(destFile);
                if (!string.IsNullOrWhiteSpace(destPath))
                {
                    if (!Directory.Exists(destPath))
                    {
                        Directory.CreateDirectory(destPath);
                    }
                }
                //经测试 .jpg 格式缩略图大小与质量等最优 
                bmp.Save(destFile, ImageFormat.Jpeg);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("从" + sourceFile + "生成缩略图到" + destFile + "出现异常:" + ex);
            }
            finally
            {
                //显示释放资源 
                imageFrom.DisposeIfNotNull();
                g.DisposeIfNotNull();
                bmp.DisposeIfNotNull();
            }
        }

        /// <summary>
        /// 给图片添加文字水印
        /// </summary>
        /// <param name="srcFile">源图片存放路径</param>
        /// <param name="destFile">目标图片存放路径</param>
        /// <param name="fontSize">字号，可超过72号的</param>
        /// <param name="throwException">出现异常时是否抛出</param>
        /// <param name="text">要添加的文字</param>
        public static bool AddWatermarkText(string srcFile, string destFile, int fontSize, string text)
        {

            if (string.IsNullOrWhiteSpace(srcFile) || string.IsNullOrWhiteSpace(destFile))
            {
                throw new Exception("源图或目标图片的存放路径都不能为空");
            }


            System.Drawing.Image img = null;
            Graphics picture = null;
            Font crFont = null;
            StringFormat StrFormat = new StringFormat();
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
            try
            {
                img = System.Drawing.Image.FromFile(srcFile);
                picture = Graphics.FromImage(img);

                SizeF crSize = new SizeF();
                crFont = new Font("arial", fontSize, FontStyle.Bold);
                crSize = picture.MeasureString(text, crFont);

                float xpos = 0;
                float ypos = 0;

                xpos = (img.Width - crSize.Width) / 2;
                ypos = (img.Height - crSize.Height) / 2;

                StrFormat.Alignment = StringAlignment.Near;
                picture.DrawString(text, crFont, semiTransBrush2, xpos + 1, ypos + 1, StrFormat);
                picture.DrawString(text, crFont, semiTransBrush, xpos, ypos, StrFormat);

                string destPath = Path.GetDirectoryName(destFile);
                if (!string.IsNullOrWhiteSpace(destPath))
                {
                    if (!Directory.Exists(destPath))
                    {
                        Directory.CreateDirectory(destPath);
                    }
                }
                img.Save(destFile);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("给图片添加水印时发生异常：" + ex);
            }
            finally
            {
                crFont.DisposeIfNotNull();
                StrFormat.DisposeIfNotNull();
                semiTransBrush2.DisposeIfNotNull();
                semiTransBrush.DisposeIfNotNull();
                picture.DisposeIfNotNull();
                img.DisposeIfNotNull();
            }


        }

        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="verificationText">验证码字符串</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片长度</param>
        /// <returns>图片base64字符串</returns>
        public static string CreateVerificationImage(string verificationText, int width, int height)
        {
            Pen _pen = new Pen(Color.Black);
            Font _font = new Font("Arial", 10, FontStyle.Bold);
            Brush _brush = null;
            Bitmap _bitmap = new Bitmap(width, height);
            Graphics _g = Graphics.FromImage(_bitmap);
            SizeF _totalSizeF = _g.MeasureString(verificationText, _font);
            SizeF _curCharSizeF;
            PointF _startPointF = new PointF((width - _totalSizeF.Width) / 2, (height - _totalSizeF.Height) / 2);
            //随机数产生器
            Random _random = new Random();



            _g.Clear(Color.White);
            for (int i = 0; i < verificationText.Length; i++)
            {
                if (i == 0)
                {
                    _startPointF.X = _startPointF.X / 2;
                }

                _brush = new LinearGradientBrush(new Point(0, 0), new Point(1, 1), Color.FromArgb(_random.Next(255), _random.Next(255), _random.Next(255)), Color.FromArgb(_random.Next(255), _random.Next(255), _random.Next(255)));
                _g.DrawString(verificationText[i].ToString(), _font, _brush, _startPointF);
                _curCharSizeF = _g.MeasureString(verificationText[i].ToString(), _font);
                _startPointF.X += _curCharSizeF.Width;

                int x1 = _random.Next(_bitmap.Width);
                int y1 = _random.Next(_bitmap.Height);
                int x2 = _random.Next(_bitmap.Width);
                int y2 = _random.Next(_bitmap.Height);
                Color clr = Color.FromArgb(_random.Next(255), _random.Next(255), _random.Next(255));
                _g.DrawLine(new Pen(clr), x1, y1, x2, y2);


                int x = _random.Next(_bitmap.Width);
                int y = _random.Next(_bitmap.Height);
                Color clrPoint = Color.FromArgb(_random.Next(255), _random.Next(255), _random.Next(255));
                _bitmap.SetPixel(x, y, clrPoint);
            }
            _g.DisposeIfNotNull();

            return ImageToBase64(_bitmap, ImageFormat.Jpeg);
        }

        /// <summary>
        /// jpg,jpeg图片在反序列化后直接保存会报GDI一般错误,所以有了此方法
        /// </summary>
        /// <param name="imageFrom">返序列化出来的图片</param>
        /// <returns></returns>
        public static Bitmap GetSaveJPEGImage(Image imageFrom)
        {
            int width = imageFrom.Width;
            int height = imageFrom.Height;

            // 创建画布 
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            // 用白色清空 
            g.Clear(Color.White);
            // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // 指定高质量、低速度呈现。 
            g.SmoothingMode = SmoothingMode.HighQuality;
            // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。 
            g.DrawImage(imageFrom, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
            g.DisposeIfNotNull();
            imageFrom.DisposeIfNotNull();
            return bmp;
        }

    }
}
