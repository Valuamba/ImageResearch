using ImageResearchNew.ImageProcessing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ikc5.TypeLibrary;

namespace ImageResearchNew.ViewModel
{
    internal class ProcessorsViewModel : BaseNotifyPropertyChanged, IProcessorsViewModel
    {

        #region ProcessorsViewModel

        public DelegateCommand ProcessorSelectedChanged { get; }


        //ObservableCollection

        #endregion
    }
}
