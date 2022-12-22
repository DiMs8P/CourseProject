using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.DataStructures;

namespace CourseProject.Calculus.LocalElements
{
    public class ComponentProvider
    {
        private Grid _grid;
        public ComponentProvider(Grid grid)
        {
            _grid = grid;
        }

        public Node GetNodeByIndex(int index)
        {
            return _grid.Nodes[index];
        }
    }
}
