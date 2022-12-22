using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CourseProject.Calculus.LocalElements.BasicFunctions;
using CourseProject.DataStructures;
using CourseProject.DataStructures.Matrixes;

namespace CourseProject.Calculus.LocalElements
{
    public class BasicFunctionsCoefficientsGenerator
    {
        private readonly Grid _grid;
        public BasicFunctionsCoefficientsGenerator(Grid grid)
        {
            _grid = grid;
        }

        public void Generate()
        {
            Matrix D = new Matrix(new double[3, 3]);
            for (int elemIndex = 0; elemIndex < _grid.Elements.Count; elemIndex += 1)
            {
                List<BasicFunction> Functions = new List<BasicFunction>();
                var determinant = CalcDeterminant(elemIndex);
                if (determinant == 0)
                {
                    throw new ArgumentException("matrix is degenerate");
                }

                D.Data[0, 0] = _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[1]].Radius * _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[2]].Angle - _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[2]].Radius * _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[1]].Angle;
                D.Data[1, 0] = _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[2]].Radius * _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle - _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius * _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[2]].Angle;
                D.Data[2, 0] = _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius * _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[1]].Angle - _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[1]].Radius * _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle;

                D.Data[0, 1] = _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[1]].Angle - _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[2]].Angle;
                D.Data[1, 1] = _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[2]].Angle - _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle;
                D.Data[2, 1] = _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle - _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[1]].Angle;

                D.Data[0, 2] = _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[2]].Radius - _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[1]].Radius;
                D.Data[1, 2] = _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius - _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[2]].Radius;
                D.Data[2, 2] = _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[1]].Radius - _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius;

                var newMatrix = D * (1 / determinant);

                for (int i = 0; i < 3; i++)
                {
                    Functions.Add(new BasicFunction(Enumerable.Range(0, newMatrix.Data.GetLength(1))
                        .Select(x => newMatrix.Data[i, x])
                        .ToArray()));
                }

                _grid.Elements[elemIndex].SetFunctions(Functions);
            }


        }

        private double CalcDeterminant(int elemIndex)
        {

            return (_grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[1]].Radius - _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius) *
                   (_grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[2]].Angle - _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle) -
                   (_grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[2]].Radius - _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius) *
                   (_grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[1]].Angle - _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle);
        }
    }
}
