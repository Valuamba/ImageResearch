using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageResearchNew.Tools
{
    public class FreeHandTool : StandartTool
    {
        public override BitmapSource Icon => new BitmapImage(new Uri("/ImageResearchNew;component/Images/cursor.png", UriKind.Relative));

        public override string ToolTip => "Свободное перемещение";

        protected override void DrawMethod(DrawingContext context, Point start, Point end, Brush brush, Pen pen)
        {
            
        }
    }
}
