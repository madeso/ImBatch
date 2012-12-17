// File: SuperImage.cs
// License: 
// Please see readme.md

namespace ImBatch
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class SuperImage
    {
        private Bitmap currentBitmap;

        public SuperImage(string path)
        {
            this.currentBitmap = new Bitmap(path);
        }

        public void Crop(int xPosition, int yPosition, int width, int height)
        {
            Bitmap temp = this.currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (xPosition + width > this.currentBitmap.Width)
            {
                width = this.currentBitmap.Width - xPosition;
            }
            if (yPosition + height > this.currentBitmap.Height)
            {
                height = this.currentBitmap.Height - yPosition;
            }
            Rectangle rect = new Rectangle(xPosition, yPosition, width, height);
            this.currentBitmap = bmap.Clone(rect, bmap.PixelFormat);
        }

        public void InsertImage(string imagePath, int xPosition, int yPosition, int width, int height)
        {
            Bitmap temp = this.currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            Graphics gr = Graphics.FromImage(bmap);
            if (!string.IsNullOrEmpty(imagePath))
            {
                var img = Image.FromFile(imagePath);
                if (width == -1)
                {
                    width = img.Width;
                }
                if (height == -1)
                {
                    height = img.Height;
                }
                Rectangle rect = new Rectangle(xPosition, yPosition, width, height);
                gr.DrawImage(img, rect);
            }
            this.currentBitmap = (Bitmap)bmap.Clone();
        }

        public void InsertShape(ShapeType shapeType, int xPosition, int yPosition, int width, int height, Color colorName)
        {
            Bitmap temp = this.currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            Graphics gr = Graphics.FromImage(bmap);
            Pen pen = new Pen(colorName);
            switch (shapeType)
            {
                case ShapeType.FilledEllipse:
                    gr.FillEllipse(pen.Brush, xPosition, yPosition, width, height);
                    break;
                case ShapeType.FilledRectangle:
                    gr.FillRectangle(pen.Brush, xPosition, yPosition, width, height);
                    break;
                case ShapeType.Ellipse:
                    gr.DrawEllipse(pen, xPosition, yPosition, width, height);
                    break;
                case ShapeType.Rectangle:
                    gr.DrawRectangle(pen, xPosition, yPosition, width, height);
                    break;
                default:
                    throw new Exception("unhandled shapetype: " + shapeType);
            }
            this.currentBitmap = (Bitmap)bmap.Clone();
        }

        public void InsertText(string text, int xPosition, int yPosition, string fontName, float fontSize, FontStyle fontStyle, Color fromColor, Color toColor)
        {
            Bitmap temp = this.currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            Graphics gr = Graphics.FromImage(bmap);
            if (string.IsNullOrEmpty(fontName))
            {
                fontName = "Times New Roman";
            }
            if (fontSize.Equals(null))
            {
                fontSize = 10.0F;
            }
            Font font = new Font(fontName, fontSize, fontStyle);
            int gW = (int)(text.Length * fontSize);
            gW = gW == 0 ? 10 : gW;
            LinearGradientBrush lgBrush = new LinearGradientBrush(new Rectangle(0, 0, gW, (int)fontSize), fromColor, toColor, LinearGradientMode.Vertical);
            gr.DrawString(text, font, lgBrush, xPosition, yPosition);
            this.currentBitmap = (Bitmap)bmap.Clone();
        }

        public void Resize(int newWidth, int newHeight)
        {
            if (newWidth != 0 && newHeight != 0)
            {
                Bitmap temp = this.currentBitmap;
                Bitmap bmap = new Bitmap(newWidth, newHeight, temp.PixelFormat);

                double nWidthFactor = temp.Width / (double)newWidth;
                double nHeightFactor = temp.Height / (double)newHeight;

                for (int x = 0; x < bmap.Width; ++x)
                {
                    for (int y = 0; y < bmap.Height; ++y)
                    {
                        int frX = (int)Math.Floor(x * nWidthFactor);
                        int frY = (int)Math.Floor(y * nHeightFactor);
                        int cx = frX + 1;
                        if (cx >= temp.Width)
                        {
                            cx = frX;
                        }
                        int cy = frY + 1;
                        if (cy >= temp.Height)
                        {
                            cy = frY;
                        }
                        double fx = x * nWidthFactor - frX;
                        double fy = y * nHeightFactor - frY;
                        double nx = 1.0 - fx;
                        double ny = 1.0 - fy;

                        Color color1 = temp.GetPixel(frX, frY);
                        Color color2 = temp.GetPixel(cx, frY);
                        Color color3 = temp.GetPixel(frX, cy);
                        Color color4 = temp.GetPixel(cx, cy);

                        // Blue
                        byte bp1 = (byte)(nx * color1.B + fx * color2.B);

                        byte bp2 = (byte)(nx * color3.B + fx * color4.B);

                        byte nBlue = (byte)(ny * (bp1) + fy * (bp2));

                        // Green
                        bp1 = (byte)(nx * color1.G + fx * color2.G);

                        bp2 = (byte)(nx * color3.G + fx * color4.G);

                        byte nGreen = (byte)(ny * (bp1) + fy * (bp2));

                        // Red
                        bp1 = (byte)(nx * color1.R + fx * color2.R);

                        bp2 = (byte)(nx * color3.R + fx * color4.R);

                        byte nRed = (byte)(ny * (bp1) + fy * (bp2));

                        bmap.SetPixel(x, y, Color.FromArgb(255, nRed, nGreen, nBlue));
                    }
                }
                this.currentBitmap = (Bitmap)bmap.Clone();
            }
        }

        public void RotateFlip(RotateFlipType rotateFlipType)
        {
            Bitmap temp = this.currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            bmap.RotateFlip(rotateFlipType);
            this.currentBitmap = (Bitmap)bmap.Clone();
        }

        public void Save(string path)
        {
            this.currentBitmap.Save(path);
        }

        public void SetBrightness(int brightness)
        {
            Bitmap temp = this.currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (brightness < -255)
            {
                brightness = -255;
            }
            if (brightness > 255)
            {
                brightness = 255;
            }
            for (var i = 0; i < bmap.Width; i++)
            {
                for (var j = 0; j < bmap.Height; j++)
                {
                    Color c = bmap.GetPixel(i, j);
                    var cR = c.R + brightness;
                    var cG = c.G + brightness;
                    var cB = c.B + brightness;

                    if (cR < 0)
                    {
                        cR = 1;
                    }
                    if (cR > 255)
                    {
                        cR = 255;
                    }

                    if (cG < 0)
                    {
                        cG = 1;
                    }
                    if (cG > 255)
                    {
                        cG = 255;
                    }

                    if (cB < 0)
                    {
                        cB = 1;
                    }
                    if (cB > 255)
                    {
                        cB = 255;
                    }

                    bmap.SetPixel(i, j, Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                }
            }
            this.currentBitmap = (Bitmap)bmap.Clone();
        }

        public void SetColorFilter(ColorFilterTypes colorFilterType)
        {
            Bitmap temp = this.currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    Color c = bmap.GetPixel(i, j);
                    var nPixelR = 0;
                    var nPixelG = 0;
                    var nPixelB = 0;
                    switch (colorFilterType)
                    {
                        case ColorFilterTypes.Red:
                            nPixelR = c.R;
                            nPixelG = c.G - 255;
                            nPixelB = c.B - 255;
                            break;
                        case ColorFilterTypes.Green:
                            nPixelR = c.R - 255;
                            nPixelG = c.G;
                            nPixelB = c.B - 255;
                            break;
                        case ColorFilterTypes.Blue:
                            nPixelR = c.R - 255;
                            nPixelG = c.G - 255;
                            nPixelB = c.B;
                            break;
                    }
                    nPixelR = Math.Max(nPixelR, 0);
                    nPixelR = Math.Min(255, nPixelR);

                    nPixelG = Math.Max(nPixelG, 0);
                    nPixelG = Math.Min(255, nPixelG);

                    nPixelB = Math.Max(nPixelB, 0);
                    nPixelB = Math.Min(255, nPixelB);

                    bmap.SetPixel(i, j, Color.FromArgb((byte)nPixelR, (byte)nPixelG, (byte)nPixelB));
                }
            }
            this.currentBitmap = (Bitmap)bmap.Clone();
        }

        public void SetContrast(double contrast)
        {
            Bitmap temp = this.currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (contrast < -100)
            {
                contrast = -100;
            }
            if (contrast > 100)
            {
                contrast = 100;
            }
            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    Color c = bmap.GetPixel(i, j);
                    double pR = c.R / 255.0;
                    pR -= 0.5;
                    pR *= contrast;
                    pR += 0.5;
                    pR *= 255;
                    if (pR < 0)
                    {
                        pR = 0;
                    }
                    if (pR > 255)
                    {
                        pR = 255;
                    }

                    double pG = c.G / 255.0;
                    pG -= 0.5;
                    pG *= contrast;
                    pG += 0.5;
                    pG *= 255;
                    if (pG < 0)
                    {
                        pG = 0;
                    }
                    if (pG > 255)
                    {
                        pG = 255;
                    }

                    double pB = c.B / 255.0;
                    pB -= 0.5;
                    pB *= contrast;
                    pB += 0.5;
                    pB *= 255;
                    if (pB < 0)
                    {
                        pB = 0;
                    }
                    if (pB > 255)
                    {
                        pB = 255;
                    }

                    bmap.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
                }
            }
            this.currentBitmap = (Bitmap)bmap.Clone();
        }

        public void SetGamma(double red, double green, double blue)
        {
            Bitmap temp = this.currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            byte[] redGamma = CreateGammaArray(red);
            byte[] greenGamma = CreateGammaArray(green);
            byte[] blueGamma = CreateGammaArray(blue);
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    Color c = bmap.GetPixel(i, j);
                    bmap.SetPixel(i, j, Color.FromArgb(redGamma[c.R], greenGamma[c.G], blueGamma[c.B]));
                }
            }
            this.currentBitmap = (Bitmap)bmap.Clone();
        }

        public void SetGrayscale()
        {
            Bitmap temp = this.currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    Color c = bmap.GetPixel(i, j);
                    byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);

                    bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            this.currentBitmap = (Bitmap)bmap.Clone();
        }

        public void SetInvert()
        {
            Bitmap temp = this.currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    Color c = bmap.GetPixel(i, j);
                    bmap.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            this.currentBitmap = (Bitmap)bmap.Clone();
        }

        private static byte[] CreateGammaArray(double color)
        {
            byte[] gammaArray = new byte[256];
            for (int i = 0; i < 256; ++i)
            {
                gammaArray[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / color)) + 0.5));
            }
            return gammaArray;
        }
    }
}
