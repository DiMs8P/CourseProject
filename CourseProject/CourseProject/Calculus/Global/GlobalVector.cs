using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.DataStructures;

namespace CourseProject.Calculus.Global
{
    public class GlobalVector
    {
        public ReadOnlySpan<double> Values => new(_values);
        private double[] _values;
        Grid _grid;

        public GlobalVector(Grid grid)
        {
            _grid = grid;
            _values = new double[_grid.Nodes.Count];
        }

        public void Init()
        {
            foreach (var elem in _grid.Elements)
            {
                for (int i = 0; i < elem.NodeIndexes.Length; i++)
                {
                    var globalIndex = elem.NodeIndexes[i];
                    _values[globalIndex] += elem.LocalVector[i];
                }
            }

        }
        public void Add(int index, double data)
        {
            if (index < 0 || index >= Values.Length)
            {
                throw new ArgumentOutOfRangeException("Index out of range");
            }

            _values[index] += data;
        }

        public void Set(int index, double data)
        {
            if (index < 0 || index >= Values.Length)
            {
                throw new ArgumentOutOfRangeException("Index out of range");
            }

            _values[index] = data;
        }
    }
}
