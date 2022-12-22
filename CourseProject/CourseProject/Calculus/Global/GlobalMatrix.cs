using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CourseProject.DataStructures;
using CourseProject.DataStructures.Matrixes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseProject.Calculus.Global
{
    public class GlobalMatrix
    {
        public double[] Values { get; set; }
        public ReadOnlySpan<int> RowPtr => new(_rowPtr);
        public ReadOnlySpan<int> ColumnPtr => new(_columnPtr);
        public ReadOnlySpan<double> Diag => new(_diag);

        private int[] _rowPtr;
        private int[] _columnPtr;
        private double[] _diag;



        Grid _grid;

        public GlobalMatrix(Grid grid)
        {
            _grid = grid;
            _diag = new double[_grid.Nodes.Count];
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

            SetIg(list);
            SetJg(list);
            Values = new double[_columnPtr.Length];
            SetValues();
        }

        public void Add(int globalIndexI, int globalIndexJ, double data)
        {
            GetData(globalIndexI, globalIndexJ) += data;
        }

        //TODO check if i < j
        public void SetValue(int globalIndexI, int globalIndexJ, double data)
        {
            GetData(globalIndexI, globalIndexJ) = data;
        }

        private ref double GetData(int i, int j)
        {
            if (j > i)
            {
                (j, i) = (i, j);
            }

            if (i == j)
                return ref _diag[i];

            var ColumnIndexPrev = i == 0 ? 0 : RowPtr[i - 1];
            var numbersNum = RowPtr[i] - ColumnIndexPrev;
            for (int k = 0; k < numbersNum; k++)
            {
                if (ColumnPtr[ColumnIndexPrev + k] == j)
                    return ref Values[ColumnIndexPrev + k];
            }

            throw new ArgumentException();
        }

        private void SetValues()
        {
            foreach (var elem in _grid.Elements)
            {
                SymmetricMatrix localMatrix = elem.MassMatrix / elem.Gamma + elem.StiffnessMatrix;
                for (int i = 0; i < elem.NodeIndexes.Length; i++)
                {
                    var globalIndex = elem.NodeIndexes[i];
                    _diag[globalIndex] += localMatrix.Data[i, i];
                }

                for (int i = 0; i < elem.NodeIndexes.Length; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        var globalIndexI = elem.NodeIndexes[i];
                        var globalIndexJ = elem.NodeIndexes[j];
                        SetValue(globalIndexI, globalIndexJ, localMatrix[i, j]);
                    }
                }
            }
        }



        public IEnumerable<IndexValue> ColumnIndexValuesByRow(int rowIndex)
        {
            if (_rowPtr.Length == 0)
                yield break;

            if (rowIndex < 0) throw new ArgumentOutOfRangeException(nameof(rowIndex));

            var end = _rowPtr[rowIndex];

            var begin = rowIndex == 0
                ? 0
                : _rowPtr[rowIndex - 1];

            for (int i = begin; i < end; i++)
                yield return new IndexValue(_columnPtr[i], Values[i]);
        }


        private void SetJg(List<SortedSet<int>> list)
        {
            _columnPtr = list.SelectMany(x => x).ToArray();
        }

        private void SetIg(List<SortedSet<int>> list)
        {
            _rowPtr = new int[list.Count];
            _rowPtr[0] = 0;
            for (int i = 1; i < list.Count; i++)
            {
                _rowPtr[i] = _rowPtr[i - 1] + list[i].Count;
            }
        }

    }
}
