using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ikc5.TypeLibrary;
using ImageResearchNew.ImageCharts;

namespace ImageResearchNew.ViewModel
{
    public class OscilloscopeViewModel : BaseNotifyPropertyChanged, IOscilloscopeViewModel
    {
        public OscilloscopeViewModel()
        {

        }

        #region OscilloscopeViewModel

        private ObservableCollection<IChart> _charts = new ObservableCollection<IChart>
        {
            new OscillogramChart(),
            new SpectrogramChart(),
            new VectoroscopeChart()
        };

        public ObservableCollection<IChart> Charts
        {
            get => _charts;
            set => SetProperty(ref _charts, value);
        }

        #endregion
    }
}
