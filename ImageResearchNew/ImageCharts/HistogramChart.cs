using LiveCharts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ikc5.TypeLibrary;
using ImageResearch.Engine;

namespace ImageResearchNew.ImageCharts
{
    public class HistogramChart : BaseNotifyPropertyChanged, IChart
    {
        public string Name => "Гистограмма";

        private ChartValues<int> _values;
        private int _colorCount;

        public ChartValues<int> Values 
        { 
            get => _values;
            set => SetProperty(ref _values, value);
        }

        public int ColorCount
        {
            get => _colorCount;
            set => SetProperty(ref _colorCount, value);
        }

        public void CalculateData(Image img)
        {
            Values = new ChartValues<int>(new VimsImageFactory().Load(img).BuildHistogram());
        }
    }
}
