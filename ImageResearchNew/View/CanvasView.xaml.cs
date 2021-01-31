using ImageResearchNew.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageResearchNew.View
{
    /// <summary>
    /// Interaction logic for CanvasView.xaml
    /// </summary>
    public partial class CanvasView : UserControl
    {
        public CanvasView()
        {
            InitializeComponent();
        }

        private void EditedImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as CanvasViewModel;

            viewModel?.OnMouseDown(this, e, e.GetPosition(EditedImage));
        }

        private void EditedImage_MouseMove(object sender, MouseEventArgs e)
        {
            var viewModel = DataContext as CanvasViewModel;

            viewModel?.OnMouseMove(this, e, e.GetPosition(EditedImage));
        }

        private void EditedImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as CanvasViewModel;

            viewModel?.OnMouseUp(this, e, e.GetPosition(EditedImage));
        }
    }
}
