using ImageResearchNew.Classes;
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
using System.Windows.Shapes;

namespace ImageResearchNew.AppWindows
{
    /// <summary>
    /// Interaction logic for PixelsWindow.xaml
    /// </summary>
    public partial class PixelsWindow : Window
    {
        public PixelsWindow()
        {
            InitializeComponent();
        }

        public void CreateGrid(Pixel[][] pixels)
        {
            PixelsGrid.RowDefinitions.Clear();
            PixelsGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < pixels[0].Length; i++)
            {
                PixelsGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int j = 0; j < pixels.Length; j++)
            {
                PixelsGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (var i = 0; i < pixels.Length; i++)
            {
                for (var j = 0; j < pixels[i].Length; j++)
                {
                    var border = new Border();

                    border.SetValue(Grid.ColumnProperty, j);
                    border.SetValue(Grid.RowProperty, i);

                    border.BorderThickness = new Thickness(1, 1, 1, 1);
                    border.BorderBrush = Brushes.Black;

                    var rectangle = new Rectangle();
                    var pixel = pixels[i][j];

                    rectangle.ToolTip = pixel.ToString();
                    rectangle.Fill = new SolidColorBrush(Color.FromArgb(pixel.Color.A, pixel.Color.R, pixel.Color.G, pixel.Color.B));
                    rectangle.SetValue(Grid.ColumnProperty, j);
                    rectangle.SetValue(Grid.RowProperty, i);
                    rectangle.Width = 20;
                    rectangle.Height = 20;

                    PixelsGrid.Children.Add(rectangle);
                    PixelsGrid.Children.Add(border);
                }
            }
        }
    }
}
