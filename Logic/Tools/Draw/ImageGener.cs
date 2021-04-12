using Discord;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Logic.Tools.Log;

namespace Zamargrad.Logic.Tools.Draw
{
    public class ImageGener
    {
        static int letterSize = 35;
        static int jumpSize = 75;
        static int hPlus = jumpSize;
        static int wPlus = 0;

        public static async Task ImageLog(IMessageChannel Chanel, string text)
        {

            GetImageSize(text);

            int lenText = text.Length;

            
            Bitmap b = new Bitmap(wPlus, hPlus);

            using (Graphics g = Graphics.FromImage(b))
            {
                SolidBrush brush = new SolidBrush(System.Drawing.Color.BlueViolet);
                g.FillRectangle(brush, 0, 0, wPlus, hPlus);

                brush.Color = System.Drawing.Color.Red;
                g.FillRectangle(brush, 10, 5, wPlus - 20, hPlus - 10);

                brush.Color = System.Drawing.Color.Black;
                Font drawFont = new Font("Arial", 48);
                StringFormat drawFormat = new StringFormat();
                g.DrawString(text, drawFont, brush, new PointF(10, 5), drawFormat);

            }

            b.Save(@"image\register.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            await Chanel.SendFileAsync(@"image\register.jpg");

        }

        static void GetImageSize(string text)
        {
            hPlus = jumpSize;
            wPlus = jumpSize;

            int newLength = 0;

            foreach (char s in text)
            {
                if (s == '\n')
                {
                    hPlus += jumpSize;

                    if (newLength > wPlus)
                    {
                        wPlus = newLength;
                    }

                    newLength = 0;
                }
                else
                {
                    newLength += letterSize;
                }

            }

            if (wPlus == jumpSize) wPlus = newLength;
        }
        
    }
}
