using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace Harry.Validation.ImageProvider
{
    public class GeneralImageProvider : IImageProvider
    {
        private readonly Random rand = new Random();

        private readonly GeneralImageOptions options;

        public GeneralImageProvider() : this(new GeneralImageOptions())
        {

        }


        public GeneralImageProvider(GeneralImageOptions options)
        {
            this.options = options;
        }

        public Bitmap Create(int width, int height, string txt)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.Clear(Color.White);
                int num = 0;
                double num2 = (double)(width / txt.Length);
                for (int i = 0; i < txt.Length; i++)
                {
                    char c = txt[i];
                    using (Font f = GetFont(height))
                    {
                        using (Brush brush = new SolidBrush(GetRandomColor()))
                        {
                            Rectangle rectangle = new Rectangle(Convert.ToInt32((double)num * num2), 0, Convert.ToInt32(num2), height);
                            GraphicsPath graphicsPath = TextPath(c.ToString(), f, rectangle);
                            WarpText(graphicsPath, rectangle, width, height);
                            graphics.FillPath(brush, graphicsPath);
                            num++;
                        }
                    }
                }
                Rectangle rect = new Rectangle(new Point(0, 0), bitmap.Size);
                AddNoise(graphics, rect);
                AddLine(graphics, rect, width, height);
                return bitmap;
            }
        }

        private string GetRandomFontFamily()
        {
            return options.RandomFontFamily[rand.Next(0, options.RandomFontFamily.Count)];
        }

        private PointF RandomPoint(int xmin, int xmax, int ymin, int ymax)
        {
            return new PointF((float)rand.Next(xmin, xmax), (float)rand.Next(ymin, ymax));
        }

        private PointF RandomPoint(Rectangle rect)
        {
            return RandomPoint(rect.Left, rect.Width, rect.Top, rect.Bottom);
        }

        private Color GetRandomColor()
        {
            return options.RandomColor[rand.Next(0, options.RandomColor.Count)];
        }

        private static GraphicsPath TextPath(string s, Font f, Rectangle r)
        {
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            };
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddString(s, f.FontFamily, (int)f.Style, f.Size, r, format);
            return graphicsPath;
        }

        private Font GetFont(int height)
        {
            string randomFontFamily = GetRandomFontFamily();
            float emSize;
            switch (options.FontWarp)
            {
                case Level.Low:
                    emSize = (float)Convert.ToInt32((double)height * 0.8);
                    break;
                case Level.Medium:
                    emSize = (float)Convert.ToInt32((double)height * 0.85);
                    break;
                case Level.High:
                    emSize = (float)Convert.ToInt32((double)height * 0.9);
                    break;
                case Level.Extreme:
                    emSize = (float)Convert.ToInt32((double)height * 0.95);
                    break;
                default:
                    emSize = (float)Convert.ToInt32((double)height * 0.7);
                    break;
            }
            return new Font(randomFontFamily, emSize, FontStyle.Bold);
        }

        private void WarpText(GraphicsPath textPath, Rectangle rect, int width, int height)
        {
            float num;
            float num2;
            switch (options.FontWarp)
            {
                default:
                    return;
                case Level.Low:
                    num = 6f;
                    num2 = 1f;
                    break;
                case Level.Medium:
                    num = 5f;
                    num2 = 1.3f;
                    break;
                case Level.High:
                    num = 4.5f;
                    num2 = 1.4f;
                    break;
                case Level.Extreme:
                    num = 4f;
                    num2 = 1.5f;
                    break;
            }
            RectangleF srcRect = new RectangleF(Convert.ToSingle(rect.Left), 0f, Convert.ToSingle(rect.Width), (float)rect.Height);
            int num3 = Convert.ToInt32((float)rect.Height / num);
            int num4 = Convert.ToInt32((float)rect.Width / num);
            int num5 = rect.Left - Convert.ToInt32((float)num4 * num2);
            int num6 = rect.Top - Convert.ToInt32((float)num3 * num2);
            int num7 = rect.Left + rect.Width + Convert.ToInt32((float)num4 * num2);
            int num8 = rect.Top + rect.Height + Convert.ToInt32((float)num3 * num2);
            if (num5 < 0)
            {
                num5 = 0;
            }
            if (num6 < 0)
            {
                num6 = 0;
            }
            if (num7 > width)
            {
                num7 = width;
            }
            if (num8 > height)
            {
                num8 = height;
            }
            PointF pointF = RandomPoint(num5, num5 + num4, num6, num6 + num3);
            PointF pointF2 = RandomPoint(num7 - num4, num7, num6, num6 + num3);
            PointF pointF3 = RandomPoint(num5, num5 + num4, num8 - num3, num8);
            PointF pointF4 = RandomPoint(num7 - num4, num7, num8 - num3, num8);
            PointF[] destPoints = new PointF[4]
            {
            pointF,
            pointF2,
            pointF3,
            pointF4
            };
            Matrix matrix = new Matrix();
            matrix.Translate(0f, 0f);
            textPath.Warp(destPoints, srcRect, matrix, WarpMode.Perspective, 0f);
        }

        private void AddNoise(Graphics g, Rectangle rect)
        {
            int num;//控制点数,值越小,点越多
            int num2;//控制噪点大小,值越小,点越大
            switch (options.BackgroundNoise)
            {
                default:
                    return;
                case Level.Low:
                    num = 30;
                    num2 = 35;
                    break;
                case Level.Medium:
                    num = 18;
                    num2 = 30;
                    break;
                case Level.High:
                    num = 16;
                    num2 = 25;
                    break;
                case Level.Extreme:
                    num = 12;
                    num2 = 20;
                    break;
            }
            SolidBrush solidBrush = new SolidBrush(GetRandomColor());
            int maxValue = Convert.ToInt32(Math.Max(rect.Width, rect.Height) / num2);
            for (int i = 0; i <= Convert.ToInt32(rect.Width * rect.Height / num); i++)
            {
                g.FillEllipse(solidBrush, rand.Next(rect.Width), rand.Next(rect.Height), rand.Next(maxValue), rand.Next(maxValue));
            }
            solidBrush.Dispose();
        }

        private void AddLine(Graphics g, Rectangle rect, int width, int height)
        {
            int num;//线条点数
            float tmpWidth;//线条宽
            int num2; //线条数
            switch (options.LineNoise)
            {
                default:
                    return;
                case Level.Low:
                    num = 2;
                    tmpWidth = Convert.ToSingle((double)height / 31.25);
                    num2 = 1;
                    break;
                case Level.Medium:
                    num = 3;
                    tmpWidth = Convert.ToSingle((double)height / 27.7777);
                    num2 = 2;
                    break;
                case Level.High:
                    num = 4;
                    tmpWidth = Convert.ToSingle(height / 25F);
                    num2 = 3;
                    break;
                case Level.Extreme:
                    num = 5;
                    tmpWidth = Convert.ToSingle((double)height / 22.7272);
                    num2 = 4;
                    break;
            }
            PointF[] array = new PointF[num + 1];


            for (int i = 1; i <= num2; i++)
            {
                using (Pen pen = new Pen(GetRandomColor(), tmpWidth))
                {
                    for (int j = 0; j <= num; j++)
                    {
                        array[j] = RandomPoint(rect);
                    }
                    g.DrawCurve(pen, array, 1.75f);
                }
            }

        }
    }
}
