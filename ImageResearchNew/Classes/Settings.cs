using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ImageResearchNew.Classes
{
    internal class Settings : BindableObjectBase
    {
        private Brush _background;
        private Brush _foreground;
        private FontFamily _font;
        private double _fontSize;
        private int _gridSize;

        private static Settings _instance;
        
        public static Settings Instance { get { return _instance; } }

        public Brush Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
                OnPropertyChanged();
            }
        }

        public FontFamily Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
                OnPropertyChanged();
            }
        }

        public double FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                OnPropertyChanged();
            }
        }

        public Brush Foreground
        {
            get
            {
                return _foreground;
            }
            set
            {
                _foreground = value;
                OnPropertyChanged();
            }
        }

        public int GridSize
        {
            get
            {
                return _gridSize;
            }
            set
            {
                _gridSize = value;
                OnPropertyChanged();
            }
        }

        static Settings()
        {
            _instance = new Settings();
        }

        Settings()
        {
            _font = SystemFonts.CaptionFontFamily;
            _fontSize = SystemFonts.CaptionFontSize;
            _foreground = SystemColors.WindowTextBrush;
            _background = SystemColors.WindowBrush;
            _gridSize = 64;
        }
    }
}
