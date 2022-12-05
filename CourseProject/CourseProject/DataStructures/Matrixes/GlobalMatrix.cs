using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataStructures.Matrixes
{
    public class GlobalMatrix : ISymmetrical
    {
        public List<int> _rowIndex = new();
        public List<int> _columnIndex = new();
        public List<float> values = new();

        Grid _grid;

        public GlobalMatrix(IGridFactory factory)
        {
            _grid = factory.CreateGrid();
            CreateMatrixPortrait();
        }

        private void CreateMatrixPortrait()
        {
            List<SortedSet<int>> list = new List<SortedSet<int>>();
            for (int i = 0; i < _grid.Nodes.Count; i++)
            {
                list.Add(new SortedSet<int>());
            }

            foreach (var element in _grid.Elements)
            {

                for (int i = 0; i < element.NodeIndexes.Length; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        list[element.NodeIndexes[i]].Add(element.NodeIndexes[j]);
                    }
                }

            }

            SetIG(list);
            SetJG(list);
        }

        private void SetJG(List<SortedSet<int>> list)
        {
            _columnIndex.Add(0);
            foreach (var relatedIndexes in list)
            {
                foreach(var index in relatedIndexes)
                {
                    _columnIndex.Add(index);
                }
            }
        }

        private void SetIG(List<SortedSet<int>> list)
        {
            _rowIndex.Add(0);
            for(int i = 1; i < list.Count; i++)
            {
                _rowIndex.Add(list[i].Count + _rowIndex[i - 1]);
            }
        }

        public ref double this[int row, int column]
        {
            get // чтение из позиции index
            {

                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
