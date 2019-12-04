using System;
using System.IO;
using System.Text;
using System.Drawing;

namespace PictureToDots
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = args.Length > 0 ? args[0] : throw new Exception("File path is not specified");
            char whitePixel = args.Length > 1 ? args[1][0] : ' ';
            char blackPixel = args.Length > 2 ? args[2][0] : '.';

            string res = BitmapToString(new Bitmap(filePath, true), whitePixel, blackPixel);  

            File.WriteAllText(Path.ChangeExtension(Path.GetFileName(filePath), ".txt"), res);
        }

        private static string BitmapToString(Bitmap bmp, char whitePixel, char blackPixel)
        {
            StringBuilder resultBuilder = new StringBuilder();

            int blackMaxValue = Math.Abs(Color.Black.ToArgb()) / 2;

            Color pixelColor;
            long argbColor;

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    pixelColor = bmp.GetPixel(x, y);
                    argbColor = Math.Abs((long)pixelColor.ToArgb());

                    if (argbColor <= blackMaxValue)
                        resultBuilder.Append(whitePixel);
                    else
                        resultBuilder.Append(blackPixel);
                }

                resultBuilder.Append(Environment.NewLine);
            }

            return resultBuilder.ToString();
        }
    }
}
