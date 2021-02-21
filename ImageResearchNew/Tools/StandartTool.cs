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
    public abstract class StandartTool : AbstractInfoTool, ICanvasCallback
    {
        private BitmapSource _original = null;
        private Point? _start = null;

        public void OnMouseDown(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
            _original = sender.EditedImage.CurrentLayer.Image;
            _start = position;
        }

        public void OnMouseMove(CanvasViewModel sender, MouseEventArgs e, Point position)
        {
            if (_start.HasValue)
            {
                sender.EditedImage.CurrentLayer.Image = ImageHelper.CreateRenderTarget(
                    (int)sender.EditedImage.CurrentLayer.Image.Width, (int)sender.EditedImage.CurrentLayer.Image.Height,
                    (visual, context) =>
                    {
                        context.DrawImage(_original, new Rect(0.0, 0.0, sender.EditedImage.CurrentLayer.Image.Width, sender.EditedImage.CurrentLayer.Image.Height));
                        DrawMethod(context, sender, e, position);
                    });
            }
        }

        private void DrawMethod(DrawingContext context, CanvasViewModel sender, MouseEventArgs e, Point position)
        {
            Brush brush = null;
            Pen pen = null;

            brush = new SolidColorBrush();
            pen = new Pen(Brushes.White, 1);

            DrawMethod(context, _start.Value, position, brush, pen);
        }

        public void OnMouseUp(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
            _start = null;
        }

        protected abstract void DrawMethod(DrawingContext context, Point start, Point end, Brush brush, Pen pen);

        public void OnMouseEnter(CanvasViewModel sender, MouseEventArgs e, Point position)
        {
        }
    }
}
