using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ikc5.TypeLibrary;

namespace ImageResearchNew.ViewModel
{
    public class DynamicGridViewModel : BaseNotifyPropertyChanged, IDynamicGridViewModel
    {
        private readonly ObservableCollection<ICellViewModel> _unformattedCells;

        public DynamicGridViewModel(ObservableCollection<ICellViewModel> cells)
        {
            this.SetDefaultValues();
            _unformattedCells = cells;
            GridCount = _unformattedCells.Count;
        }

        private ObservableCollection<ObservableCollection<ICellViewModel>> CreateCellsStructure()
        {
            if (GridCount != 0)
            {
                int counter = 0;
                GridWidth = (int)Math.Ceiling(Math.Sqrt(GridCount));
                GridHeight = (int)Math.Ceiling(GridCount / (double)GridWidth);

                var cells = new ObservableCollection<ObservableCollection<ICellViewModel>>();
                for (var posRow = 0; posRow < GridHeight; posRow++)
                {
                    var row = new ObservableCollection<ICellViewModel>();
                    for (var posCol = 0; posCol < GridWidth && counter < GridCount; posCol++, counter++)
                    {
                        var cellViewModel = _unformattedCells[counter];
                        row.Add(cellViewModel);
                    }
                    cells.Add(row);
                }
                return cells;
            }
            return null;
        }

        #region DynamicGridViewModel

        private ObservableCollection<ObservableCollection<ICellViewModel>> _cells;
        private int _gridHeight;
        private int _gridWidth;
        private int _gridCount;
            
        public ObservableCollection<ObservableCollection<ICellViewModel>> Cells
        {
            get { return _cells; }
            set
            {
                SetProperty(ref _cells, value);
                OnPropertyChanged(nameof(GridCount));
            }
        }

        public int GridWidth
        {
            get { return _gridWidth; }
            set { SetProperty(ref _gridWidth, value); }
        }

        public int GridHeight
        {
            get { return _gridHeight; }
            set { SetProperty(ref _gridHeight, value); }
        }

        public int GridCount
        {
            get { return _gridCount; }
            set
            {
                SetProperty(ref _gridCount, value);
                Cells = CreateCellsStructure();
            }
        }

        #endregion
    }
}
