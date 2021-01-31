using ImageResearchNew.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace ImageResearchNew.Helpers
{
    public static class ImageHelper
    {
        public static EditedImage Open(FileInfo file)
        {
            try
            {
                // Selecting a Way to load the Image depending on it's Format:
                switch (file.Extension.ToLower())
                {
                    case ".bmp":
                    case ".png":
                    case ".jpg":
                    case ".jpeg":
                    case ".gif":
                    case ".tif": return LoadBitmap(file);
                    default:
                        throw new ArgumentException();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        private static BitmapSource BitmapSourceFromBitmap(System.Drawing.Bitmap source)
        {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs = null;

            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }

            return bs;
        }

        public static System.Drawing.Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            System.Drawing.Bitmap bitmap;

            using (var outStream = new MemoryStream())
            {
                var enc = new BmpBitmapEncoder();

                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
            }
            return bitmap;
        }

        private static EditedImage LoadBitmap(FileInfo file)
        {
            var sourceImage = (Bitmap)Image.FromFile(file.FullName);
            var image = new EditedImage(sourceImage);
            var bitmap = BitmapSourceFromBitmap(sourceImage);

            //var bitmap = new BitmapImage(new Uri(file.FullName));
            //var image = new EditedImage(BitmapFromSource(bitmap));

            image.Width = (int)bitmap.Width;
            image.Height = (int)bitmap.Height;
            image.AddLayer();
            image.CurrentLayer.Image = bitmap;

            return image;
        }

        public static BitmapSource CreateRenderTarget(int width, int height, Action<DrawingVisual, DrawingContext> drawAction = null)
        {
            var visual = new DrawingVisual();

            using (var context = visual.RenderOpen())
            {
                drawAction?.Invoke(visual, context);
            }

            var bitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);

            bitmap.Render(visual);

            return bitmap;
        }

        public static void Save(FileInfo file, EditedImage image)
        {
            try
            {
                // Selecting a Way to save the Image depending on it's Format:
                switch (file.Extension.ToLower())
                {
                    case ".bmp": SaveBitmap(file, image, new BmpBitmapEncoder()); break;
                    case ".png": SaveBitmap(file, image, new PngBitmapEncoder()); break;
                    case ".jpg":
                    case ".jpeg": SaveBitmap(file, image, new JpegBitmapEncoder()); break;
                    case ".gif": SaveBitmap(file, image, new GifBitmapEncoder()); break;
                    case ".tif": SaveBitmap(file, image, new TiffBitmapEncoder()); break;
                    default:
                        throw new ArgumentException();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static void SaveBitmap(FileInfo file, EditedImage image, BitmapEncoder encoder)
        {
            encoder.Frames.Add(BitmapFrame.Create(image.Image));

            var output = File.Open(file.FullName, FileMode.OpenOrCreate, FileAccess.Write);

            encoder.Save(output);
            output.Close();
        }
    }
}
