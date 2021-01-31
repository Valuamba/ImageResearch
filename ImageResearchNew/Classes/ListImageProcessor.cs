using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew
{
    public abstract class ListImageProcessor : ImageProcessor
    {
        private ImageProcessor _selectedProcessor;
        private readonly ObservableCollection<ImageProcessor> _processors;

        public ObservableCollection<ImageProcessor> Items => _processors;

        public ImageProcessor SelectedItem
        {
            get
            {
                return _selectedProcessor;
            }
            set
            {
                _selectedProcessor = value;
                OnPropertyChanged();
            }
        }

        public ListImageProcessor(List<ImageProcessor> processors)
        {
            _processors = new ObservableCollection<ImageProcessor>(processors);

            SelectedItem = processors.FirstOrDefault();
        }
    }
}
