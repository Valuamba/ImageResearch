using ImageResearchNew.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Bitmap = System.Drawing.Bitmap;

namespace ImageResearchNew.Model
{
    public class EditedImage : BindableObjectBase
    {
        private Bitmap _sourceImage;
        private BitmapSource _image = null;
        private ObservableCollection<ImageLayer> _layers = new ObservableCollection<ImageLayer>();
        private ImageLayer _currentLayer = null;

        private int _width = 10;
        private int _height = 10;
        private int _layerIndex = 0;

        private string _name;
        public string Name
        {
            get => _name;
        }

        public Bitmap SourceImage { get { return _sourceImage; } }

        public BitmapSource Image
        {
            get { return _image; }
            private set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ImageLayer> Layers
        {
            get { return _layers; }
            set
            {
                _layers = value;
                OnPropertyChanged();
            }
        }

        public ImageLayer CurrentLayer
        {
            get { return _currentLayer; }
            set
            {
                _currentLayer = value;
                OnPropertyChanged();
            }
        }

        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        //public int DpiX { get { return (int)_sourceImage.HorizontalResolution; } }

        //public int DpiY { get { return (int)_sourceImage.VerticalResolution; } }

        public EditedImage(Bitmap sourceImage, string name)
        {
            _sourceImage = sourceImage;
            _name = name;
        }

        public void AddLayer()
        {
            AddLayer(string.Format("Layer {0}", ++_layerIndex));
        }

        public void AddLayer(string name)
        {
            var layer = new ImageLayer();

            layer.PropertyChanged += Layer_PropertyChanged;

            layer.Image = ImageHelper.CreateRenderTarget(Width, Height);
            layer.Name = name;
            layer.Visible = true;

            Layers.Insert(0, layer);
            CurrentLayer = layer;
            UpdateImage();
        }

        public void RemoveLayer()
        {
            RemoveLayer(CurrentLayer.Name);
        }

        public void RemoveLayer(string name)
        {
            var layer = Layers.FirstOrDefault(s => s.Name == name);

            if (layer != null)
            {
                layer.PropertyChanged -= Layer_PropertyChanged;

                Layers.Remove(layer);
                CurrentLayer = Layers[0];
                UpdateImage();
            }
        }

        public bool ContainsLayer(string name)
        {
            return Layers.Any(s => s.Name == name);
        }

        public ImageLayer GetLayerBy(string name)
        {
            return Layers.FirstOrDefault(s => s.Name == name);
        }

        public void MoveCurrentLayer(int newIndex)
        {
            var layer = CurrentLayer;

            Layers.Remove(layer);
            Layers.Insert(newIndex, layer);

            CurrentLayer = layer;
            UpdateImage();
        }

        private void UpdateImage()
        {
            var drawingRectangle = new Rect(0, 0, Width, Height);

            Image = ImageHelper.CreateRenderTarget(Width, Height,
                (visual, context) =>
                {
                    for (int x = 0; x < drawingRectangle.Width / 10; x++)
                    {
                        for (int y = 0; y < drawingRectangle.Height / 10; y++)
                        {
                            if ((x + y) % 2 == 0)
                            {
                                context.DrawRectangle(Brushes.LightGray, null, new Rect(new Point(10 * x, 10 * y), new Point(10 * (x + 1), 10 * (y + 1))));
                            }
                        }
                    }

                    for (int i = _layers.Count - 1; i >= 0; i--)
                    {
                        if (_layers[i].Visible)
                        {
                            context.DrawImage(_layers[i].Image, drawingRectangle);
                        }
                    }
                });
        }

        private void Layer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Image" || e.PropertyName == "Visible")
            {
                UpdateImage();
            }

            OnPropertyChanged(e.PropertyName);
        }
    }
}
