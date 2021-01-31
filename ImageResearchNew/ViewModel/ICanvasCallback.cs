using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ImageResearchNew.ViewModel
{
    public interface ICanvasCallback
    {
        void OnMouseDown(CanvasViewModel sender, MouseButtonEventArgs e, Point position);

        void OnMouseUp(CanvasViewModel sender, MouseButtonEventArgs e, Point position);

        void OnMouseMove(CanvasViewModel sender, MouseEventArgs e, Point position);
    }
}
