using CourseProject.Calculus.Global;
using CourseProject.DataStructures;
using CourseProject.DataStructures.BoundaryCondition;
using CourseProject.DataStructures.Matrixes;
using CourseProject.Readers;
using System;

namespace CourseProject.BoundaryConditions
{
    internal class SecondBoundaryConditionsApplyer
    {
        private IReader<SecondBoundaryCondition> _reader;
        private double[] _rootsLegendrePolynomial = new double[] { -0.5773503, 0.5773503 };
        private double[] _weights = new double[] { 1.0, 1.0 };
        private readonly int _gaussSplitsNumber = 2;
        private readonly int _numberOfSplits = 512;
        private Grid _grid;

        public SecondBoundaryConditionsApplyer(Grid grid, IReader<SecondBoundaryCondition> reader)
        {
            _reader = reader;
            _grid = grid;
        }

        public void Apply(GlobalVector globalVector)
        {
            var allBoundatyConditions = _reader.Read();

            foreach (var boundaryCondition in allBoundatyConditions)
            {
                bool bounderIsHorizontal =
                    (boundaryCondition.SecondLocalIndex - boundaryCondition.FirstLocalIndex) == 1;

                var boundaryConditio = bounderIsHorizontal ? CalcR(_grid.ElemHeight, boundaryCondition) : CalcPhi(_grid.ElemWidth, boundaryCondition);

                globalVector.Add(_grid.Elements[boundaryCondition.ElemIndex].NodeIndexes[boundaryCondition.FirstLocalIndex], boundaryConditio[0]);
                globalVector.Add(_grid.Elements[boundaryCondition.ElemIndex].NodeIndexes[boundaryCondition.SecondLocalIndex], boundaryConditio[1]);
            }
        }

        public double[] CalcPhi(double elemLength, SecondBoundaryCondition boundaryCondition)
        {
            double heightStep = elemLength / _numberOfSplits;
            double[] secondCondition = new double[2];

            var globalIndex1 = _grid.Elements[boundaryCondition.ElemIndex]
                .NodeIndexes[boundaryCondition.FirstLocalIndex];

            var globalIndex2 = _grid.Elements[boundaryCondition.ElemIndex]
                .NodeIndexes[boundaryCondition.SecondLocalIndex];

            int[] index = new int[2] { boundaryCondition .FirstLocalIndex, boundaryCondition.SecondLocalIndex};

            for (int p = 0; p < 2; p++)
            {
                for (int j = 0; j < _gaussSplitsNumber; j++)
                {
                    var sumOfInnerIntegral = 0.0d;
                    for (int phi = 0; phi < _numberOfSplits; phi++)
                    {
                        var phiJ = (_grid.Nodes[globalIndex1].Angle +
                                    phi * heightStep +
                                    _grid.Nodes[globalIndex1].Angle +
                                    (phi + 1) * heightStep) / 2.0d +
                                   _rootsLegendrePolynomial[j] * heightStep / 2;

                        sumOfInnerIntegral += heightStep *
                                              ((_grid.Elements[boundaryCondition.ElemIndex].Functions[boundaryCondition.FirstLocalIndex].ValueIn(_grid.Nodes[globalIndex1].Radius, phiJ) * boundaryCondition.Theta1) + (
                                               _grid.Elements[boundaryCondition.ElemIndex].Functions[boundaryCondition.SecondLocalIndex].ValueIn(_grid.Nodes[globalIndex2].Radius, phiJ) * boundaryCondition.Theta2)) *
                                              _grid.Elements[boundaryCondition.ElemIndex].Functions[index[p]].ValueIn(_grid.Nodes[globalIndex1].Radius, phiJ) *
                                               _grid.Nodes[globalIndex1].Radius * _grid.Nodes[globalIndex1].Radius;
                    }

                    secondCondition[p] = sumOfInnerIntegral * _weights[j] / 2.0;
                }
            }

            return secondCondition;
        }


        public double[] CalcR(double elemLength, SecondBoundaryCondition boundaryCondition)
        {
            double widthStep = elemLength / _numberOfSplits;
            double[] secondCondition = new double[2];

            var globalIndex1 = _grid.Elements[boundaryCondition.ElemIndex]
                .NodeIndexes[boundaryCondition.FirstLocalIndex];

            var globalIndex2 = _grid.Elements[boundaryCondition.ElemIndex]
                .NodeIndexes[boundaryCondition.SecondLocalIndex];

            int[] index = new int[2] { boundaryCondition.FirstLocalIndex, boundaryCondition.SecondLocalIndex };

            for (int p = 0; p < 2; p++)
            {
                for (int j = 0; j < _gaussSplitsNumber; j++)
                {
                    var sumOfInnerIntegral = 0.0d;
                    for (int r = 0; r < _numberOfSplits; r++)
                    {
                        var rJ = (_grid.Nodes[globalIndex1].Radius +
                                    r * widthStep +
                                    _grid.Nodes[globalIndex1].Radius +
                                    (r + 1) * widthStep) / 2.0d +
                                   _rootsLegendrePolynomial[j] * widthStep / 2;

                        sumOfInnerIntegral += widthStep *
                                              ((_grid.Elements[boundaryCondition.ElemIndex].Functions[boundaryCondition.FirstLocalIndex].ValueIn(rJ, _grid.Nodes[globalIndex1].Angle) * boundaryCondition.Theta1) + (
                                               _grid.Elements[boundaryCondition.ElemIndex].Functions[boundaryCondition.SecondLocalIndex].ValueIn(rJ, _grid.Nodes[globalIndex1].Angle) * boundaryCondition.Theta2)) *
                                              _grid.Elements[boundaryCondition.ElemIndex].Functions[index[p]].ValueIn(rJ, _grid.Nodes[globalIndex1].Angle) *
                                              rJ * rJ;
                    }

                    secondCondition[p] = sumOfInnerIntegral * _weights[j] / 2.0;
                }
            }

            return secondCondition;
        }
    }
}
