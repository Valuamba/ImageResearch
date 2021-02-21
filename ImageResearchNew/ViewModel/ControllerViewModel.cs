using ImageResearchNew.Classes;
using ImageResearchNew.Helpers;
using ImageResearchNew.ImageProcessing;
using ImageResearchNew.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew.ViewModel
{
    public class ControllerViewModel : BindableObjectBase
    {
        private bool _isShowGrid;
        private CanvasViewModel _canvas;
        private ToolboxViewModel _toolBox;

        public int ScaleValue
        {
            get => Canvas != null
                ? (int) (Canvas.Width * Canvas.Height)
                : 0;
            set
            {
                Canvas.Height = Math.Sqrt((double)(value * 0.75));
                Canvas.Width = Math.Sqrt((double)(value * 1.33));
                OnPropertyChanged();
            }
        }

        public CanvasViewModel Canvas
        {
            get { return _canvas; }
            set
            {
                _canvas = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ImageProcessor> _imageProcessors = new ObservableCollection<ImageProcessor>()
        {
            new FormatProcessor(
                new List<ImageProcessor>()
                {
                    new RGBProcessor(),
                    new YUVProcessor()
                }),
            new BrightnessProcessor(),
            new ContrastProcessor(),
            new SubsamplingRateProcessor(),
        };

        public ObservableCollection<ImageProcessor> ImageProcessors
        {
            get { return _imageProcessors; }
            set
            {
                _imageProcessors = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand ProcessorValueChanged { get; }
        public DelegateCommand ProcessorSelectedChanged { get; }
        public DelegateCommand ShowGridCommand { get; }
        public DelegateCommand OpenImageCommand { get; }
        public DelegateCommand SelectedItemToolChanged { get; }
        public DelegateCommand ToolSummaryResult { get; }
        public DelegateCommand ScaleValueChanged { get; }

        public ToolboxViewModel ToolBox
        {
            get
            {
                return _toolBox;
            }

            set
            {
                _toolBox = value;
                OnPropertyChanged();
            }
        }

        public bool IsShowGrid
        {
            get
            {
                return _isShowGrid;
            }

            set
            {
                _isShowGrid = value;
                OnPropertyChanged();
            }
        }

        public ControllerViewModel()
        {
            ProcessorValueChanged = new DelegateCommand(obj => OnProcessorValueChanged(obj), obj => Canvas != null);
            ProcessorSelectedChanged = new DelegateCommand(obj => OnProcessorSelectedChanged(obj), obj => Canvas != null);
            ShowGridCommand = new DelegateCommand(obj => ShowGrid(obj), obj => Canvas != null);
            OpenImageCommand = new DelegateCommand(obj => OpenImage());
            SelectedItemToolChanged = new DelegateCommand(obj => OnSelectedItemToolChanged(obj));
            ToolSummaryResult = new DelegateCommand(obj => SummaryResult(), obj => Canvas != null);
            ScaleValueChanged = new DelegateCommand(obj => OnScaleValueChanged(obj), obj => Canvas != null);

            Settings.Instance.PropertyChanged += Instance_PropertyChanged;

            _toolBox = new ToolboxViewModel();
        }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "GridSize")
            {
                var layer = Canvas.EditedImage.GetLayerBy("Grid");

                if (layer != null)
                {
                    CreateGridImage();
                }
            }
        }

        private void SummaryResult()
        {
            var tool = Canvas.Toolbox.CurrentTool as IToolContainer;

            if (tool != null)
            {
                tool.ShowResult(Canvas);
            }
        }

        private int delta = 0;
        private void OnScaleValueChanged(object obj)
        {
            //if (delta < _scaleValue)
            //{
            //    Canvas.Width++;
            //    Canvas.Height++;
            //}
            //else
            //{
            //    Canvas.Width--;
            //    Canvas.Height--;
            //}
            //delta = _scaleValue;

        }

        private void OnSelectedItemToolChanged(object obj)
        {
            var toolbox = obj as ToolboxViewModel;

            if (toolbox != null)
            {
                toolbox.Tools.OfType<IToolContainer>().ToList().ForEach(s => s.Clear(Canvas));
            }
        }

        private void OpenImage()
        {
            FileInfo file = null;

            if (DialogHelper.ShowOpenFileDialog(out file))
            {
                var image = ImageHelper.Open(file);

                if (image == null)
                {
                    return;
                }

                var canvas = new CanvasViewModel(image)
                {
                    Toolbox = ToolBox
                };

                Canvas = canvas;
                Canvas.EditedImage.AddLayer("Grid");
                CreateGridImage();
                Canvas.EditedImage.CurrentLayer.Visible = IsShowGrid;
                OnPropertyChanged(nameof(ControllerViewModel.ScaleValue));
            }
        }

        private void CreateGridImage()
        {
            Canvas.EditedImage.CurrentLayer.Image = DrawHelper.DrawGrid(Canvas.EditedImage.CurrentLayer, Settings.Instance.GridSize);
        }

        private void ShowGrid(object obj)
        {
            var state = (bool)obj;

            if (state)
            {
                var layer = Canvas.EditedImage.Layers.FirstOrDefault(s => s.Name == "Grid");

                if (layer != null)
                {
                    layer.Visible = true;
                }
            }
            else
            {
                Canvas.EditedImage.Layers.FirstOrDefault(s => s.Name == "Grid").Visible = false;
            }
        }

        private void OnProcessorValueChanged(object obj)
        {
            var processor = obj as ImageProcessor;

            if (processor != null)
            {
                processor.Process(Canvas.EditedImage);
            }

            Console.WriteLine("Value: " + obj);
        }

        public void OnProcessorSelectedChanged(object obj)
        {
            var processor = obj as ImageProcessor;

            if (processor != null)
            {
                processor.Process(Canvas.EditedImage);
            }

            Console.WriteLine("Selection: " + obj);
        }
    }
}
