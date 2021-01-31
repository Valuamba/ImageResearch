using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResearchNew.Model.Classes
{
    internal class GraphContainer
    {
        public List<object> AxisX { get; private set; }
        public List<GraphButton> Buttons { get; private set; }

        public GraphContainer(List<object> axisX, List<GraphButton> buttons)
        {
            AxisX = axisX;
            Buttons = buttons;
        }
    }
}
