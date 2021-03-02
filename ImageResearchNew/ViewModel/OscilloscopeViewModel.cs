using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ikc5.TypeLibrary;
using ImageResearchNew.ImageCharts;
using ImageResearchNew.Model;

namespace ImageResearchNew.ViewModel
{
    public class OscilloscopeViewModel : BaseNotifyPropertyChanged, IOscilloscopeViewModel
    {
        private EditedImage currentImage;

        public OscilloscopeViewModel(EditedImage currentImage)
        {
            this.currentImage = currentImage;
            SelectedChartChanged = new DelegateCommand(obj => OnSelectedChartChanged(obj));
        }

        public DelegateCommand SelectedChartChanged { get; }

        public void OnSelectedChartChanged(object obj)
        {
            var chart = obj as IChart;

            if(chart != null)
            {
                chart.CalculateData(currentImage.SourceImage);
            }
        }

        #region OscilloscopeViewModel

        private IChart _selectedChart;
        private ObservableCollection<IChart> _charts = new ObservableCollection<IChart>
        {
            new OscillogramChart(),
            new SpectrogramChart(),
            new VectoroscopeChart(),
            new HistogramChart()
        };

        public IChart SelectedChart
        {
            get => _selectedChart;
            set => SetProperty(ref _selectedChart, value);
        }

        public ObservableCollection<IChart> Charts
        {
            get => _charts;
            set => SetProperty(ref _charts, value);
        }
        #endregion
    }
}
