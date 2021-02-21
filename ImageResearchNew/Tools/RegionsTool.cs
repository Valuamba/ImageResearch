using ImageResearchNew.AppWindows;
using ImageResearchNew.Classes;
using ImageResearchNew.Helpers;
using ImageResearchNew.Model;
using ImageResearchNew.Model.Classes;
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
    public class RegionsTool : AbstractInfoTool, ICanvasCallback, IToolContainer
    {
        private const string Layername = "Regions";
        private BitmapSource _original = null;
        private Point? _start = null;
        private List<Rect> _selectedRegions = new List<Rect>();

        public override BitmapSource Icon => new BitmapImage(new Uri("/ImageResearchNew;component/Images/rectangle.png", UriKind.Relative));

        public override string ToolTip => "Выделение областей и получение графика";

        public void Clear(CanvasViewModel sender)
        {
            sender.EditedImage.RemoveLayer(Layername);
            _selectedRegions = new List<Rect>();
        }

        public void ShowResult(CanvasViewModel sender)
        {
            var region = _selectedRegions.FirstOrDefault();

            if (region != null)
            {
                var axisX = new List<object>();
                var pixels = new List<System.Drawing.Color>();
                var rButton = new GraphButton("R");
                var gButton = new GraphButton("G");
                var bButton = new GraphButton("B");
                var width = Math.Min(region.X + region.Width, sender.EditedImage.SourceImage.Width);

                for (var i = region.X; i < width; i++)
                {
                    var pixel = sender.EditedImage.SourceImage.GetPixel((int)i, (int)region.Y);

                    axisX.Add(i);

                    rButton.AddItem(new GraphItem(i, pixel.R));
                    gButton.AddItem(new GraphItem(i, pixel.G));
                    bButton.AddItem(new GraphItem(i, pixel.B));

                    pixels.Add(sender.EditedImage.SourceImage.GetPixel((int)i, (int)region.Y));
                }

                var continer = new GraphContainer(axisX, new List<GraphButton>() { rButton, gButton, bButton });

                if (axisX.Count > 0)
                {
                    var window = new GraphWindow();

                    window.DataContext = continer;

                    window.ShowDialog();
                }
            }
        }

        public void OnMouseDown(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
            if (!Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Clear(sender);
            }

            if (!sender.EditedImage.ContainsLayer(Layername))
            {
                sender.EditedImage.AddLayer(Layername);
            }

            _original = sender.EditedImage.CurrentLayer.Image;
            _start = position;
        }

        public void OnMouseMove(CanvasViewModel sender, MouseEventArgs e, Point position)
        {
            if (_start.HasValue)
            {
                Draw(sender, position);
            }
        }

        public void OnMouseUp(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
            if (_start.HasValue)
            {
                var region = GetRegion(position);

                Draw(sender, position);

                _selectedRegions.Add(region);
            }

            _start = null;
        }

        private void Draw(CanvasViewModel sender, Point position)
        {
            sender.EditedImage.CurrentLayer.Image = ImageHelper.CreateRenderTarget(
                (int)sender.EditedImage.CurrentLayer.Image.Width, (int)sender.EditedImage.CurrentLayer.Image.Height,
                (visual, context) =>
                {
                    context.DrawImage(_original, new Rect(0.0, 0.0, sender.EditedImage.CurrentLayer.Image.Width, sender.EditedImage.CurrentLayer.Image.Height));

                    var brush = new SolidColorBrush(Colors.Blue) { Opacity = 0.3 };

                    context.DrawRectangle(brush, new Pen(), GetRegion(position));
                });
        }

        private Rect GetRegion(Point position)
        {
            var gridSize = Settings.Instance.GridSize;

            var startX = (int)(_start.Value.X / gridSize) * gridSize;
            var startY = (int)(_start.Value.Y / gridSize) * gridSize;
            var endX = (int)(position.X / gridSize) * gridSize + gridSize;
            var endY = (int)(position.Y / gridSize) * gridSize + gridSize;
            var rect = new Rect(startX, startY, Math.Max(endX - startX, gridSize), Math.Max(endY - startY, gridSize));

            return rect;
        }

        public void OnMouseEnter(CanvasViewModel sender, MouseEventArgs e, Point position)
        {
        }
    }
}
