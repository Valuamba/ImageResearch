using ImageResearchNew.AppWindows;
using ImageResearchNew.Classes;
using ImageResearchNew.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ImageResearchNew.Tools
{
    public class RegionTool : AbstractInfoTool, ICanvasCallback
    {
        private bool _clicked;

        public override BitmapSource Icon => new BitmapImage(new Uri("/ImageResearchNew;component/Images/click.png", UriKind.Relative));

        public override string ToolTip => "Просмотр пикселей региона";

        public void OnMouseDown(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
            _clicked = true;
        }

        public void OnMouseMove(CanvasViewModel sender, MouseEventArgs e, Point position)
        {
            
        }

        public void OnMouseUp(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
            if (_clicked)
            {
                var gridSize = Settings.Instance.GridSize;
                var startX = (int)(position.X / gridSize) * gridSize;
                var startY = (int)(position.Y / gridSize) * gridSize;

                var pixels = GetPixelsRegion(sender, startX, startY, gridSize);

                var window = new PixelsWindow();

                window.CreateGrid(pixels);
                window.ShowDialog();
            }

            _clicked = false;
        }

        private Pixel[][] GetPixelsRegion(CanvasViewModel sender, int x, int y, int blockSize)
        {
            var image = sender.EditedImage.SourceImage;
            var pixels = new List<List<Pixel>>();

            var imageX = x / blockSize * blockSize;
            var imageY = y / blockSize * blockSize;

            for (int i = 0; i < pixels.Count; i++)
            {
                pixels[i] = new List<Pixel>();
            }

            for (var j = imageY; j < imageY + blockSize - 1; j++)
            {
                pixels.Add(new List<Pixel>());

                for (var i = imageX; i < imageX + blockSize - 1; i++)
                {
                    if (i < image.Width && j < image.Height)
                    {
                        pixels.Last().Add(new Pixel(image.GetPixel(i, j)));
                    }
                }
            }

            return pixels.Select(a => a.ToArray()).ToArray();
        }
    }
}
