using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageResearchNew.ViewImageTools
{
    public interface IViewCallback
    {
        void OnMouseDown(object sender, MouseButtonEventArgs e);

        void OnMouseUp(object sender, MouseButtonEventArgs e);
    }
}
