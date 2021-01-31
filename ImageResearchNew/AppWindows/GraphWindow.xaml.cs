using ImageResearchNew.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImageResearchNew.AppWindows
{
    /// <summary>
    /// Interaction logic for GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        private static List<System.Drawing.Color> Colors = new List<System.Drawing.Color>()
        {
            System.Drawing.Color.Red,
            System.Drawing.Color.Green,
            System.Drawing.Color.Blue,
        };

        public GraphWindow()
        {
            InitializeComponent();
        }

        private void CreateSeries(string name, List<object> keys, List<object> values)
        {
            var seriesName = "Series" + name;
            var series1 = new Series(seriesName);

            series1.ChartArea = "Default";
            series1.ChartType = SeriesChartType.Line;
            series1.MarkerColor = Colors[Math.Min(Colors.Count, Chart.Series.Count)];
            series1.Color = Colors[Math.Min(Colors.Count, Chart.Series.Count)];
            series1.BorderWidth = 3;
            series1.Font = new System.Drawing.Font(series1.Font.Name, 8, System.Drawing.FontStyle.Bold);
            series1.LabelBackColor = System.Drawing.Color.GhostWhite;
            series1.MarkerStyle = MarkerStyle.Circle;
            series1.MarkerSize = 10;
            series1.LabelToolTip = "#VAL";
            series1.LegendText = name;
            series1.IsVisibleInLegend = false;
            series1.SmartLabelStyle.Enabled = false;

            Chart.Legends[0].CustomItems.Add(Colors[Math.Min(Colors.Count, Chart.Series.Count)], name);

            Chart.Series.Add(series1);

            Chart.Series[seriesName].Points.DataBindXY(keys, values);
        }

        private void CreatePlot()
        {
            Chart.ChartAreas.Clear();
            Chart.Series.Clear();
            Chart.Legends[0].CustomItems.Clear();

            Chart.ChartAreas.Add(new ChartArea("Default"));

            Chart.ChartAreas[0].AxisX2.Enabled = AxisEnabled.False;
        }

        private void CreateCharts()
        {
            var container = DataContext as GraphContainer;

            CreatePlot();

            foreach (var b in container.Buttons)
            {
                if (b.IsSelected)
                {
                    CreateSeries(b.Name, container.AxisX, b.Items.Select(s => s.Value).OfType<object>().ToList());
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = ((FrameworkElement)sender).DataContext as GraphButton;

            button.IsSelected = !button.IsSelected;

            CreateCharts();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var legend1 = new Legend("Default");

            legend1.LegendStyle = LegendStyle.Row;
            legend1.Docking = Docking.Bottom;
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.Font = new System.Drawing.Font(legend1.Font.Name, 14, System.Drawing.FontStyle.Bold);
            legend1.ShadowOffset = 5;
            legend1.BorderWidth = 2;
            legend1.BorderColor = System.Drawing.Color.Gray;

            Chart.Legends.Add(legend1);

            CreateCharts();
        }

        private void Chart_GetToolTipText(object sender, System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs e)
        {
            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.DataPoint:
                    {
                        var chart = sender as Chart;

                        if (chart != null)
                        {
                            e.Text += "--- " + e.HitTestResult.Series.Points[e.HitTestResult.PointIndex].XValue + " ---" + Environment.NewLine;

                            e.Text += string.Format("{0}: {1}", e.HitTestResult.Series.LegendText, e.HitTestResult.Series.Points[e.HitTestResult.PointIndex].YValues[0]);
                        }
                        break;
                    }
                case ChartElementType.Nothing:
                    {
                        break;
                    }
                default:
                    {
                        var chart = sender as Chart;
                        var data = chart.HitTest(e.X, e.Y);

                        if (data != null && data.ChartArea != null)
                        {
                            var xValue = (int)data.ChartArea.AxisX.PixelPositionToValue(e.X);

                            e.Text += "--- " + xValue + " ---" + Environment.NewLine;

                            foreach (var serie in chart.Series)
                            {
                                var point = serie.Points.FirstOrDefault(s => s.XValue == xValue);

                                if (point != null)
                                {
                                    e.Text += string.Format("{0}: {1}", serie.LegendText, point.YValues[0]) + Environment.NewLine;
                                }
                            }
                        }

                        break;
                    }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Console.WriteLine(DateTime.Now + " Closing");
        }
    }
}
