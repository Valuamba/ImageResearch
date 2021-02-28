using Ikc5.TypeLibrary;
using ImageResearchNew.AppWindows;
using ImageResearchNew.Classes;
using ImageResearchNew.Helpers;
using ImageResearchNew.ImageProcessing;
using ImageResearchNew.Model;
using ImageResearchNew.ViewImageTools;
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
    public class ControllerViewModel : BaseNotifyPropertyChanged
    {
        private bool _isShowGrid;
        private CanvasViewModel _canvas;
        private ToolboxViewModel _toolBox;

        private ObservableCollection<CanvasViewModel> canvasList = new ObservableCollection<CanvasViewModel>();
        private ObservableCollection<ICellViewModel> selectedImagesViews;
        private ObservableCollection<ObservableCollection<CanvasViewModel>> _gridCanvasList;

        public ObservableCollection<CanvasViewModel> CanvasList { get => canvasList; }

        public ObservableCollection<ICellViewModel> SelectedImagesViews
        {
            get => selectedImagesViews;
            set { SetProperty(ref selectedImagesViews, value);  }
        }

        public ObservableCollection<ObservableCollection<CanvasViewModel>> GridCanvasList
        {
            get => _gridCanvasList;
            set => SetProperty(ref _gridCanvasList, value);
        }

        public int ScaleValue
        {
            get => FocusedCanvas != null
                ? (int) (FocusedCanvas.Width * FocusedCanvas.Height)
                : 0;
            set
            {
                FocusedCanvas.Height = Math.Sqrt((double)(value * 0.75));
                FocusedCanvas.Width = Math.Sqrt((double)(value * 1.33));
                OnPropertyChanged();
            }
        }

        public ToolboxViewModel ToolBox
        {
            get => _toolBox;
            set => SetProperty(ref _toolBox, value);
        }

        public bool IsShowGrid
        {
            get => _isShowGrid;
            set => SetProperty(ref _isShowGrid, value);
        }

        public CanvasViewModel FocusedCanvas
        {
            get { return _canvas; }
            set => SetProperty(ref _canvas, value);
        }

        private ObservableCollection<AbstractInfoViewTool> viewMethods = new ObservableCollection<AbstractInfoViewTool>()
        {
            new CompareViewTool(),
            new RotationIncreaseViewTool()
        };

        public ObservableCollection<AbstractInfoViewTool> ViewMethods
        {
            get => viewMethods;
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
            set => SetProperty(ref _imageProcessors, value);
        }

        public DelegateCommand ProcessorValueChanged { get; }
        public DelegateCommand ProcessorSelectedChanged { get; }
        public DelegateCommand ShowGridCommand { get; }
        public DelegateCommand OpenImageCommand { get; }
        public DelegateCommand SelectedItemToolChanged { get; }
        public DelegateCommand ToolSummaryResult { get; }
        public DelegateCommand ScaleValueChanged { get; }
        public DelegateCommand TriggerOscilloscope { get; }

        public ControllerViewModel()
        {
            ProcessorValueChanged = new DelegateCommand(obj => OnProcessorValueChanged(obj), obj => FocusedCanvas != null);
            ProcessorSelectedChanged = new DelegateCommand(obj => OnProcessorSelectedChanged(obj), obj => FocusedCanvas != null);
            ShowGridCommand = new DelegateCommand(obj => ShowGrid(obj), obj => FocusedCanvas != null);
            OpenImageCommand = new DelegateCommand(obj => OpenImage());
            SelectedItemToolChanged = new DelegateCommand(obj => OnSelectedItemToolChanged(obj));
            ToolSummaryResult = new DelegateCommand(obj => SummaryResult(), obj => FocusedCanvas != null);
            TriggerOscilloscope = new DelegateCommand(obj => OpenOscilloscope());

            Settings.Instance.PropertyChanged += Instance_PropertyChanged;

            _toolBox = new ToolboxViewModel();
        }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "GridSize")
            {
                var layer = FocusedCanvas.EditedImage.GetLayerBy("Grid");

                if (layer != null)
                {
                    CreateGridImage();
                }
            }
        }

        private void OpenOscilloscope()
        {
            var window = new OscilloscopeWindow();
            window.DataContext = new OscilloscopeViewModel();
            window.Show();
        }

        private void SummaryResult()
        {
            var tool = FocusedCanvas.Toolbox.CurrentTool as IToolContainer;

            if (tool != null)
            {
                tool.ShowResult(FocusedCanvas);
            }
        }

        private void OnSelectedItemToolChanged(object obj)
        {
            var toolbox = obj as ToolboxViewModel;

            if (toolbox != null)
            {
                toolbox.Tools.OfType<IToolContainer>().ToList().ForEach(s => s.Clear(FocusedCanvas));
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

                var focusedCanvas = new CanvasViewModel(image)
                {
                    Toolbox = ToolBox
                };

                FocusedCanvas = focusedCanvas;
                FocusedCanvas.EditedImage.AddLayer("Grid");
                CreateGridImage();
                FocusedCanvas.EditedImage.CurrentLayer.Visible = IsShowGrid;
                CanvasList.Add(FocusedCanvas);
                OnPropertyChanged(nameof(ControllerViewModel.ScaleValue));
            }
        }

        private void CreateGridImage()
        {
            FocusedCanvas.EditedImage.CurrentLayer.Image = DrawHelper.DrawGrid(FocusedCanvas.EditedImage.CurrentLayer, Settings.Instance.GridSize);
        }

        private void ShowGrid(object obj)
        {
            var state = (bool)obj;

            if (state)
            {
                var layer = FocusedCanvas.EditedImage.Layers.FirstOrDefault(s => s.Name == "Grid");

                if (layer != null)
                {
                    layer.Visible = true;
                }
            }
            else
            {
                FocusedCanvas.EditedImage.Layers.FirstOrDefault(s => s.Name == "Grid").Visible = false;
            }
        }

        public void OnViewMethodIsSelected(object selectedItem)
        {
            var viewMethod = selectedItem as AbstractInfoViewTool;

            //viewMethod.OpenCompareWindow((IList<object>)SelectedImagesViews);

            var window = new CompareWindow();
            var obj = new DynamicGridViewModel(SelectedImagesViews);
            window.DataContext = obj;
            window.Show();
        }

        private void OnProcessorValueChanged(object obj)
        {
            var processor = obj as ImageProcessor;

            if (processor != null)
            {
                processor.Process(FocusedCanvas.EditedImage);
            }

            Console.WriteLine("Value: " + obj);
        }

        public void OnProcessorSelectedChanged(object obj)
        {
            var processor = obj as ImageProcessor;

            if (processor != null)
            {
                processor.Process(FocusedCanvas.EditedImage);
            }

            Console.WriteLine("Selection: " + obj);
        }
    }
}
