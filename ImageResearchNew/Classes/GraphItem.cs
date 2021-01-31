using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew.Model.Classes
{
    internal class GraphItem
    {
        public object Key { get; private set; }
        public object Value { get; private set; }

        public GraphItem(object key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}
