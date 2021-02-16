using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace cropimage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var sourceImageBytes = File.ReadAllBytes(@"images/inputimage.jpeg");
            var cropRectangle = new Rectangle
            {
                X = 725,
                Y = 120,
                Width = 150,
                Height = 170
            };
            var croppedImageBytes = Program.CropImage(sourceImageBytes, cropRectangle);
            File.WriteAllBytes("images/outputimage.jpeg", croppedImageBytes);
        }

        public static byte[] CropImage(byte[] sourceImageByteArr, Rectangle cropRectangle)
        {
            using (MemoryStream sourceStream = new MemoryStream(sourceImageByteArr))
            {
                Bitmap sourceImage = Image.FromStream(sourceStream) as Bitmap;
                using (MemoryStream targetStream = new MemoryStream())
                {
                    using (var targetImage = new Bitmap(cropRectangle.Width, cropRectangle.Height))
                    {
                        using (Graphics g = Graphics.FromImage(targetImage))
                        {
                            g.DrawImage(sourceImage, new Rectangle(0, 0, targetImage.Width, targetImage.Height), cropRectangle, GraphicsUnit.Pixel);
                        }

                        targetImage.Save(targetStream, ImageFormat.Png);
                        return targetStream.ToArray();
                    }
                }
            }
        }
    }
}
