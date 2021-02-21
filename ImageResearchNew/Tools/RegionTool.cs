using ImageResearchNew.AppWindows;
using ImageResearchNew.Classes;
using ImageResearchNew.Helpers;
using ImageResearchNew.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageResearchNew.Tools
{
    public class RegionTool : AbstractInfoTool, ICanvasCallback
    {
        private bool _clicked;
        private BitmapSource _original = null;

        public override BitmapSource Icon => new BitmapImage(new Uri("/ImageResearchNew;component/Images/click.png", UriKind.Relative));

        public override string ToolTip => "Просмотр пикселей региона";

        public void OnMouseDown(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
            _clicked = true;
        }

        public void OnMouseMove(CanvasViewModel sender, MouseEventArgs e, Point position)
        {
            
        }

        public void OnMouseEnter(CanvasViewModel sender, MouseEventArgs e, Point position)
        {
            _original = sender.EditedImage.CurrentLayer.Image;
            Draw(sender, position);
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
                //row
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

        delegate void ActionRef<T1, T2>(ref T1 item1, ref T2 item2);

        private void Draw(CanvasViewModel sender, Point position)
        {
            sender.EditedImage.CurrentLayer.Image = ImageHelper.CreateRenderTarget(
                (int)sender.EditedImage.CurrentLayer.Image.Width, (int)sender.EditedImage.CurrentLayer.Image.Height,
                (visual, context) =>
                {
                    context.DrawImage(_original, new Rect(0.0, 0.0, sender.EditedImage.CurrentLayer.Image.Width, sender.EditedImage.CurrentLayer.Image.Height));

                    var brush = new SolidColorBrush(Colors.Blue) { Opacity = 0.3 };

                    var rects = FourContiguity(position);
                    for (int i = 0; i < rects.Count; i++)
                    {
                        context.DrawRectangle(brush, new Pen(), rects[i]);
                    }
                });
        }

        private List<Rect> FourContiguity(Point position)
        {
            var rectList = new List<Rect>();
            var gridSize = Settings.Instance.GridSize;

            var startX = (int)(position.X / gridSize) * gridSize;
            var startY = (int)(position.Y / gridSize) * gridSize;

            var f = new List<ActionRef<Vector, Vector>>()
            {
                (ref Vector vecX, ref Vector vecY) => { vecX.Negate(); vecY.Normalize(); },
                (ref Vector vecX, ref Vector vecY) => { vecX.Negate(); vecY.Negate(); },
                (ref Vector vecX, ref Vector vecY) => { vecX.Normalize(); vecY.Normalize(); },
                (ref Vector vecX, ref Vector vecY) => { vecX.Normalize(); vecY.Negate(); }
            };

            Vector vectorX = new Vector(gridSize, gridSize);
            Vector vectorY = new Vector(gridSize, gridSize);

            foreach (var action in f)
            {
                action(ref vectorX, ref vectorY);
                Vector vector = Vector.Add(vectorX, vectorY);
                rectList.Add(new Rect(new Point(startX, startY), vector));
            }

            return rectList;
        }
    }
}
