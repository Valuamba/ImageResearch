using ImageResearchNew.Classes;
using ImageResearchNew.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ImageResearchNew.ViewModel
{
    public class CanvasViewModel : BindableObjectBase
    {
        private ToolboxViewModel _toolbox = null;

        private EditedImage editedImage = null;

        public EditedImage EditedImage
        {
            get { return editedImage; }
            private set
            {
                editedImage = value;
                OnPropertyChanged();
            }
        }

        public ToolboxViewModel Toolbox
        {
            get { return _toolbox; }
            set { _toolbox = value; }
        }

        public CanvasViewModel(EditedImage editedImage)
        {
            EditedImage = editedImage;
        }

        public void OnMouseDown(object sender, MouseButtonEventArgs e, Point position)
        {
            if (_toolbox != null && _toolbox.CurrentTool != null)
            {
                _toolbox.CurrentTool.OnMouseDown(this, e, position);
            }
        }

        public void OnMouseUp(object sender, MouseButtonEventArgs e, Point position)
        {
            if (_toolbox != null && _toolbox.CurrentTool != null)
            {
                _toolbox.CurrentTool.OnMouseUp(this, e, position);
            }
        }

        public void OnMouseMove(object sender, MouseEventArgs e, Point position)
        {
            if (_toolbox != null && _toolbox.CurrentTool != null)
            {
                _toolbox.CurrentTool.OnMouseMove(this, e, position);
            }
        }
    }
}
