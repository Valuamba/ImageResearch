using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew.Model.Classes
{
    internal class GraphButton : BindableObjectBase
    {
        private bool _isSelected = true;

        public string Name { get; private set; }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public List<GraphItem> Items { get; private set; }

        public GraphButton(string name)
        {
            Name = name;
            Items = new List<GraphItem>();
        }

        public void AddItem(GraphItem item)
        {
            Items.Add(item);
        }
    }
}
