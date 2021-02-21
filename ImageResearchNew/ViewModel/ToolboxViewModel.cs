using ImageResearchNew.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew.ViewModel
{
    public class ToolboxViewModel : BindableObjectBase
    {
        private ICanvasCallback _currentTool = null;

        private ObservableCollection<ICanvasCallback> _tools = new ObservableCollection<ICanvasCallback>()
        {
            new FreeHandTool(),
            new RegionsTool(),
            new RegionTool(),
            new ContiguityTool()
        };

        public ObservableCollection<ICanvasCallback> Tools
        {
            get { return _tools; }
            set
            {
                _tools = value;
                OnPropertyChanged();
            }
        }

        public ICanvasCallback CurrentTool
        {
            get { return _currentTool; }
            set
            {
                _currentTool = value;
                OnPropertyChanged();
            }
        }

        public ToolboxViewModel()
        {
            CurrentTool = Tools[0];
        }
    }
}
