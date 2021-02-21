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
    public class ContiguityTool : AbstractInfoTool, ICanvasCallback
    {
        private BitmapSource _original = null;

        public override BitmapSource Icon => new BitmapImage(new Uri("/ImageResearchNew;component/Images/click.png", UriKind.Relative));

        public override string ToolTip => "Просмотр смежных пикселей";

        public ContiguityTool()
        {

        }

        public void OnMouseDown(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
        }

        public void OnMouseEnter(CanvasViewModel sender, MouseEventArgs e, Point position)
        {
            _original = sender.EditedImage.CurrentLayer.Image;
            Draw(sender, position);
            //_selectedRegions.Add(region);

        }

        public void OnMouseMove(CanvasViewModel sender, MouseEventArgs e, Point position)
        {
        }

        public void OnMouseUp(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
        }

        private void Draw(CanvasViewModel sender, Point position)
        {
            sender.EditedImage.CurrentLayer.Image = ImageHelper.CreateRenderTarget(
                (int)sender.EditedImage.CurrentLayer.Image.Width, (int)sender.EditedImage.CurrentLayer.Image.Height,
                (visual, context) =>
                {
                    context.DrawImage(_original, new Rect(0.0, 0.0, sender.EditedImage.CurrentLayer.Image.Width, sender.EditedImage.CurrentLayer.Image.Height));

                    var brush = new SolidColorBrush(Colors.Blue) { Opacity = 0.3 };

                    var rects = EightContiguity(position);
                    rects.AddRange(FourContiguity(position));
                    for (int i = 0; i < rects.Count; i++)
                    {
                        context.DrawRectangle(brush, new Pen(), rects[i]);
                    }
                });
        }

        delegate Vector ActionVector<T1>(T1 item1);
        delegate Point ActionPoint<T1>(T1 item1);

        private List<ActionVector<Vector>> FourContiguityVectorPattern = new List<ActionVector<Vector>>()
        {
            (Vector vec) =>  { vec.X = -vec.X; return vec; } ,
            (Vector vec) => { vec.Y = -vec.Y; return vec; }
        };

        private List<ActionVector<Vector>> EightContiguityVectorPattern = new List<ActionVector<Vector>>()
        {
            (Vector vec) => { vec.X = -vec.X; return vec; } ,
            (Vector vec) => { vec.Y = -vec.Y; return vec; },
            (Vector vec) => { vec.Y = -vec.Y; vec.X = -vec.X; return vec; },
            (Vector vec) => vec
        };

        private List<ActionPoint<Point>> EightContiguityPointPattern = new List<ActionPoint<Point>>()
        {
            (point) => point,
            (point) => {point.Offset(0, Settings.Instance.GridSize); return point; },
            (point) => {point.Offset(Settings.Instance.GridSize, 0); return point; },
            (point) => {point.Offset(Settings.Instance.GridSize, Settings.Instance.GridSize); return point; },
        };

        private Dictionary<ActionPoint<Point>, ActionVector<Vector>> EightContiguityPattern = new Dictionary<ActionPoint<Point>, ActionVector<Vector>>()
        {
            { (point) => point, (vec) => { vec.Y = -vec.Y; vec.X = -vec.X; return vec; }},
            { (point) => {point.Offset(0, Settings.Instance.GridSize); return point; }, (vec) => { vec.X = -vec.X; return vec; } },
            { (point) => {point.Offset(Settings.Instance.GridSize, 0); return point; }, (vec) => { vec.Y = -vec.Y; return vec; }},
            { (point) => {point.Offset(Settings.Instance.GridSize, Settings.Instance.GridSize); return point; }, (vec) => vec }
        };

        private List<Rect> EightContiguity(Point position)
        {
            var rectList = new List<Rect>();
            var gridSize = Settings.Instance.GridSize;

            var startX = (int)(position.X / gridSize) * gridSize;
            var startY = (int)(position.Y / gridSize) * gridSize;

            Point point = new Point(startX, startY);
            Vector vect = new Vector(gridSize, gridSize);

            foreach (var blockPattern in EightContiguityPattern)
            {
                rectList.Add(new Rect(blockPattern.Key(point), blockPattern.Value(vect)));
            }

            return rectList;
        }

        private List<Rect> FourContiguity(Point position)
        {
            var rectList = new List<Rect>();
            var gridSize = Settings.Instance.GridSize;

            var startX = (int)(position.X / gridSize) * gridSize;
            var startY = (int)(position.Y / gridSize) * gridSize;

            var points = new List<Func<Point>>()
            {
                () => new Point(startX, startY),
                () => new Point(startX + gridSize, startY + gridSize),
            };

            var f = new List<ActionVector<Vector>>()
            {
                (Vector vec) =>  { vec.X = -vec.X; return vec; } ,
                (Vector vec) => { vec.Y = -vec.Y; return vec; }
            };

            Vector vect = new Vector(gridSize, gridSize);

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < f.Count; j++)
                {
                    rectList.Add(new Rect(points[i](), f[j](vect)));
                }
            }

            return rectList;
        }
    }
}
