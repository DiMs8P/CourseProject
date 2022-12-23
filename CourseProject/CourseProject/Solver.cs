using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Calculus.Global;
using CourseProject.DataStructures;
using FileGenerators;
using FileGenerators.DataStructures;

namespace CourseProject
{
    public class Solver
    {
        private Grid _grid;
        private double[] _weights;
        private GlobalMatrix _matrix;
        private GlobalVector _vector;
        private double _eps = 0.0001; 

        public Solver(Grid grid, GlobalMatrix matrix, GlobalVector vector, double[] weights)
        {
            _grid = grid;
            _matrix = matrix;
            _vector = vector;
            _weights = weights;
        }

        public double Solve(Point point)
        {
            int elemIndex = ElemIndexByPoint(point);

            double solution = 0;
            for (int i = 0; i < _grid.Elements[elemIndex].NodeIndexes.Length; i++)
            {
                solution += _weights[_grid.Elements[elemIndex].NodeIndexes[i]] *
                            _grid.Elements[elemIndex].Functions[i].ValueIn(point.R, point.Phi);
            }

            return solution;
        }

        private int ElemIndexByPoint(Point point)
        {
            double pointHeight = point.Phi - Config.Location.LowerLeft.Phi;
            double pointWidth = point.R - Config.Location.LowerLeft.R;

            int elemIndexR = Convert.ToInt32(pointWidth / _grid.ElemWidth);
            elemIndexR = elemIndexR >= Config.NumOfX ? elemIndexR - 1 : elemIndexR;

            int elemIndexPhi = Convert.ToInt32(pointHeight / _grid.ElemHeight) ;
            elemIndexPhi = elemIndexPhi >= Config.NumOfY ? elemIndexPhi - 1 : elemIndexPhi;

            int rectangleIndex = elemIndexPhi * Config.NumOfX + elemIndexR;

            int triangleIndex = rectangleIndex * 2;

            if (point.Phi - _grid.Nodes[_grid.Elements[triangleIndex + 1].NodeIndexes[0]].Angle < _eps)
            {
                return triangleIndex;
            }

            double sidesRatio = _grid.ElemWidth / _grid.ElemHeight;
            double pointRatio = (_grid.Nodes[_grid.Elements[triangleIndex + 1].NodeIndexes[0]].Radius - point.R) / (point.Phi - _grid.Nodes[_grid.Elements[triangleIndex + 1].NodeIndexes[0]].Angle);

            return pointRatio > sidesRatio ? triangleIndex : triangleIndex + 1;
        }
    }
}
