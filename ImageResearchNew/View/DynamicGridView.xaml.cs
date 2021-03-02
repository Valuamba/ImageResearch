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
    /// Interaction logic for DynamicGridView.xaml
    /// </summary>
    public partial class DynamicGridView : UserControl
    {
        public DynamicGridView()
        {
            InitializeComponent();
        }

        //protected override Size MeasureOverride(Size constraint)
        //{
        //    if (constraint.Width == double.PositiveInfinity || constraint.Height == double.PositiveInfinity)
        //    {
        //        return Size.Empty;
        //    }

        //    return constraint;
        //}
    }
}
