// -----------------------------------------------------------------------
// <copyright file="ImageProcess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DotNet.SpiderApplication.Service
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    using OCR.TesseractWrapper;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ImageProcess
    {
        public static Bitmap toGray(this Bitmap bm)
        {
            Color c = new Color();
            int t;

            //转变为灰度图像矩阵
            for (int j = 0; j < bm.Height; j++)
            {
                for (int i = 0; i < bm.Width; i++)
                {
                    c = bm.GetPixel(i, j);
                    t = (int)((c.R + c.G + c.B) / 3.0);

                    bm.SetPixel(i, j, Color.FromArgb(t, t, t));
                }
            }
            return bm;
        }

        /// <summary>
        /// 剪裁 -- 用GDI+
        /// </summary>
        /// <param name="b">原始Bitmap</param>
        /// <param name="StartX">开始坐标X</param>
        /// <param name="StartY">开始坐标Y</param>
        /// <param name="iWidth">宽度</param>
        /// <param name="iHeight">高度</param>
        /// <returns>剪裁后的Bitmap</returns>
        public static Bitmap KiCut(Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }

            int w = b.Width;
            int h = b.Height;

            if (StartX >= w || StartY >= h)
            {
                return null;
            }

            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }

            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }

            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();

                return bmpOut;
            }
            catch
            {
                return null;
            }
        }

        //双线性插值算法
        public static Bitmap biLinear(this Bitmap bm, double p)
        {
            int iw = bm.Width;
            int ih = bm.Height;
            int ow = (int)(p * iw);          //计算目标图宽高
            int oh = (int)(p * ih);
            Bitmap obm = new Bitmap(ow, oh); //目标图像

            Color c = new Color();

            for (int j = 0; j < oh - 1; j++)
            {
                double dy = j / p;
                int iy = (int)dy;
                if (iy > ih - 2) iy = ih - 2;
                double d_y = dy - iy;

                for (int i = 0; i < ow - 1; i++)
                {
                    int r, g, b;
                    double dx = i / p;
                    int ix = (int)dx;
                    if (ix > iw - 2) ix = iw - 2;
                    double d_x = dx - ix;

                    //f(i+u,j+v)=(1-u)(1-v)f(i,j)+u(1-v)f(i+1,j)+
                    //           (1-u)vf(i,j+1)+uvf(i+1,j+1)
                    r = (int)((1 - d_x) * (1 - d_y) * (bm.GetPixel(ix, iy)).R
                      + d_x * (1 - d_y) * (bm.GetPixel(ix + 1, iy)).R
                      + (1 - d_x) * d_y * (bm.GetPixel(ix, iy + 1)).R
                      + d_x * d_y * (bm.GetPixel(ix + 1, iy + 1)).R);

                    g = (int)((1 - d_x) * (1 - d_y) * (bm.GetPixel(ix, iy)).G
                      + d_x * (1 - d_y) * (bm.GetPixel(ix + 1, iy)).G
                      + (1 - d_x) * d_y * (bm.GetPixel(ix, iy + 1)).G
                      + d_x * d_y * (bm.GetPixel(ix + 1, iy + 1)).G);

                    b = (int)((1 - d_x) * (1 - d_y) * (bm.GetPixel(ix, iy)).B
                      + d_x * (1 - d_y) * (bm.GetPixel(ix + 1, iy)).B
                      + (1 - d_x) * d_y * (bm.GetPixel(ix, iy + 1)).B
                      + d_x * d_y * (bm.GetPixel(ix + 1, iy + 1)).B);

                    if (r < 0) r = 0;
                    if (r > 255) r = 255;
                    if (g < 0) g = 0;
                    if (g > 255) g = 255;
                    if (b < 0) b = 0;
                    if (b > 255) b = 255;
                    c = Color.FromArgb(r, g, b);
                    obm.SetPixel(i, j, c);
                }
            }
            return obm;
        }

        public static string Recognize(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream st = response.GetResponseStream();
            if (st == null)
            {
                return string.Empty;
            }

            try
            {
                Bitmap bitmap = ((Bitmap)Bitmap.FromStream(st)).toGray().biLinear(2);
                //var newBitmap= KiCut(bitmap, 12, 22, bitmap.Width - 12, 22);
                string tessdata = Environment.CurrentDirectory + "\\tessdata\\";
                string language = "eng";//设置训练文件的名称，后缀traineddata之前的名称
                int oem = 3;
                // http://www.lixin.me/blog/2012/05/26/29536 训练
                using (TesseractProcessor processor = new TesseractProcessor())
                {
                    //初始化
                    bool initFlage = processor.Init(tessdata, language, oem);
                    //processor.GetTesseractEngineVersion();//获取版本号

                    /*
                     Set the current page segmentation mode. Defaults to PSM_SINGLE_BLOCK. 
                    The mode is stored as an IntParam so it can also be modified by ReadConfigFile or SetVariable("tessedit_pageseg_mode", mode as string). */
                    //processor.SetPageSegMode(ePageSegMode.PSM_SINGLE_BLOCK);

                    //设置ROI（图像的感兴趣区域）
                    processor.UseROI = true;
                    processor.ROI = new Rectangle(24, 0, bitmap.Width - 24, bitmap.Height);

                    //设置识别的变量 如果是自定义培训的文件 可以不用设置
                    //必须在初始化后调用
                    processor.SetVariable("tessedit_char_whitelist", "0123456789.");
                    //processor.SetVariable("tessedit_thresholding_method", "1"); //图像处理阀值是否打开
                    //processor.SetVariable("save_best_choices", "T");
                    using (Bitmap bmp = bitmap)
                    {
                        int i = 3;
                        oem = i;
                        string text = processor.Recognize(bmp);
                        char[] charsToTrim = { '\\', 'n', '\\', 'n' };
                        return text.TrimEnd(charsToTrim);
                        //Console.WriteLine(
                        //    string.Format(
                        //        "RecognizeMode: {1}\nText:\n{0}\n++++++\n", text, ((eOcrEngineMode)oem).ToString()));
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string Recognize(string url,Rectangle rectangle)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream st = response.GetResponseStream();
            if (st == null)
            {
                return string.Empty;
            }

            Bitmap bitmap = ((Bitmap)Bitmap.FromStream(st)).toGray().biLinear(2);
            //var newBitmap= KiCut(bitmap, 12, 22, bitmap.Width - 12, 22);
            string tessdata = Environment.CurrentDirectory + "\\tessdata\\";
            string language = "eng";//设置训练文件的名称，后缀traineddata之前的名称
            int oem = 3;
            // http://www.lixin.me/blog/2012/05/26/29536 训练
            using (TesseractProcessor processor = new TesseractProcessor())
            {
                //初始化
                bool initFlage = processor.Init(tessdata, language, oem);
                //processor.GetTesseractEngineVersion();//获取版本号

                /*
                 Set the current page segmentation mode. Defaults to PSM_SINGLE_BLOCK. 
                The mode is stored as an IntParam so it can also be modified by ReadConfigFile or SetVariable("tessedit_pageseg_mode", mode as string). */
                //processor.SetPageSegMode(ePageSegMode.PSM_SINGLE_BLOCK);

                //设置ROI（图像的感兴趣区域）
                if (rectangle != null)
                {
                    processor.UseROI = true;
                    processor.ROI = rectangle;
                }
                
                //设置识别的变量 如果是自定义培训的文件 可以不用设置
                //必须在初始化后调用
                processor.SetVariable("tessedit_char_whitelist", "0123456789.");
                //processor.SetVariable("tessedit_thresholding_method", "1"); //图像处理阀值是否打开
                //processor.SetVariable("save_best_choices", "T");
                using (Bitmap bmp = bitmap)
                {
                    string text = processor.Recognize(bmp);
                    char[] charsToTrim = { '\\', 'n', '\\', 'n' };
                    return text.TrimEnd(charsToTrim);
                }
            }
        }

    }
}
