using CourseProject.Calculus.Global;
using CourseProject.DataStructures.BoundaryCondition;
using CourseProject.DataStructures;
using CourseProject.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.DataStructures.BoundaryConditions;
using CourseProject.DataStructures.Matrixes;

namespace CourseProject.BoundaryConditions
{
    internal class ThirdBoundaryConditionsApplyer
    {
        private int _numOfNodes = 2;
        private IReader<ThirdBoundaryCondition> _reader;
        public ThirdBoundaryConditionsApplyer(IReader<ThirdBoundaryCondition> reader)
        {
            _reader = reader;
        }

        public void ApplyVector(Grid grid, GlobalVector globalVector)
        {
            var allBoundatyConditions = _reader.Read();

            foreach (var boundaryCondition in allBoundatyConditions)
            {
                bool bounderIsHorizontal =
                    (boundaryCondition.SecondGlobalIndex - boundaryCondition.FirstGlobalIndex) == 1;

                var elemLength = bounderIsHorizontal ? grid.ElemWidth : grid.ElemHeight;

                globalVector.Add(boundaryCondition.FirstGlobalIndex, elemLength * boundaryCondition.Betta * boundaryCondition.U / 2);
                globalVector.Add(boundaryCondition.SecondGlobalIndex, elemLength * boundaryCondition.Betta * boundaryCondition.U / 2);
            }
        }

        public void ApplyMatrix(Grid grid, GlobalMatrix globalMatrix)
        {
            var allBoundatyConditions = _reader.Read();

            foreach (var boundaryCondition in allBoundatyConditions)
            {
                bool bounderIsHorizontal =
                    (boundaryCondition.SecondGlobalIndex - boundaryCondition.FirstGlobalIndex) == 1;

                var elemLength = bounderIsHorizontal ? grid.ElemWidth : grid.ElemHeight;

                Matrix matrix = new Matrix(new double[2, 2]);
                matrix[0, 0] = 2;
                matrix[1, 1] = 2;
                matrix[0, 1] = 1;
                matrix[1, 0] = 1;

                matrix = matrix * boundaryCondition.Betta * elemLength / 6;

                globalMatrix.Add(boundaryCondition.FirstGlobalIndex, boundaryCondition.FirstGlobalIndex, matrix[0, 0]);
                globalMatrix.Add(boundaryCondition.SecondGlobalIndex, boundaryCondition.SecondGlobalIndex, matrix[1, 1]);
                globalMatrix.Add(boundaryCondition.FirstGlobalIndex, boundaryCondition.SecondGlobalIndex, matrix[1, 0]);
            }
        }
    }
}
