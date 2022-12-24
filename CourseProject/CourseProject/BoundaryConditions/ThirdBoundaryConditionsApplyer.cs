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
        private double[] _rootsLegendrePolynomial = new double[] { -0.5773503, 0.5773503 };
        private double[] _weights = new double[] { 1.0, 1.0 };
        private readonly int _gaussSplitsNumber = 2;
        private readonly int _numberOfSplits = 512;
        private IReader<ThirdBoundaryCondition> _reader;
        private Grid _grid;
        public ThirdBoundaryConditionsApplyer(Grid grid, IReader<ThirdBoundaryCondition> reader)
        {
            _reader = reader;
            _grid = grid;
        }

        public void ApplyVector(GlobalVector globalVector)
        {
            var allBoundatyConditions = _reader.Read();

            foreach (var boundaryCondition in allBoundatyConditions)
            {
                bool bounderIsHorizontal =
                    (boundaryCondition.SecondLocalIndex - boundaryCondition.FirstLocalIndex) == 1;

                double[] condition = bounderIsHorizontal ? CalcR(_grid.ElemWidth, boundaryCondition) : CalcPhi(_grid.ElemHeight, boundaryCondition);

                globalVector.Add(_grid.Elements[boundaryCondition.ElemIndex].NodeIndexes[boundaryCondition.FirstLocalIndex], condition[0]);
                globalVector.Add(_grid.Elements[boundaryCondition.ElemIndex].NodeIndexes[boundaryCondition.SecondLocalIndex], condition[1]);
            }
        }

        public void ApplyMatrix(GlobalMatrix globalMatrix)
        {
            var allBoundatyConditions = _reader.Read();

            foreach (var boundaryCondition in allBoundatyConditions)
            {
                bool bounderIsHorizontal =
                    (boundaryCondition.SecondLocalIndex - boundaryCondition.FirstLocalIndex) == 1;

                var matrix = bounderIsHorizontal
                    ? GetHorizontalBounderMatrix(_grid.ElemWidth, boundaryCondition)
                    : GetVerticalBounderMatrix(_grid.ElemHeight, boundaryCondition);

                globalMatrix.Add(_grid.Elements[boundaryCondition.ElemIndex].NodeIndexes[boundaryCondition.FirstLocalIndex], _grid.Elements[boundaryCondition.ElemIndex].NodeIndexes[boundaryCondition.FirstLocalIndex], matrix[0, 0]);
                globalMatrix.Add(_grid.Elements[boundaryCondition.ElemIndex].NodeIndexes[boundaryCondition.SecondLocalIndex], _grid.Elements[boundaryCondition.ElemIndex].NodeIndexes[boundaryCondition.SecondLocalIndex], matrix[1, 1]);
                globalMatrix.Add(_grid.Elements[boundaryCondition.ElemIndex].NodeIndexes[boundaryCondition.FirstLocalIndex], _grid.Elements[boundaryCondition.ElemIndex].NodeIndexes[boundaryCondition.SecondLocalIndex], matrix[1, 0]);
            }
        }

        public double[] CalcPhi(double elemLength, ThirdBoundaryCondition boundaryCondition)
        {
            double heightStep = elemLength / _numberOfSplits;
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
                    for (int phi = 0; phi < _numberOfSplits; phi++)
                    {
                        var phiJ = (_grid.Nodes[globalIndex1].Angle +
                                    phi * heightStep +
                                    _grid.Nodes[globalIndex1].Angle +
                                    (phi + 1) * heightStep) / 2.0d +
                                   _rootsLegendrePolynomial[j] * heightStep / 2;

                        sumOfInnerIntegral += heightStep *
                                              ((_grid.Elements[boundaryCondition.ElemIndex].Functions[boundaryCondition.FirstLocalIndex].ValueIn(_grid.Nodes[globalIndex1].Radius, phiJ) * boundaryCondition.U1) + (
                                               _grid.Elements[boundaryCondition.ElemIndex].Functions[boundaryCondition.SecondLocalIndex].ValueIn(_grid.Nodes[globalIndex1].Radius, phiJ) * boundaryCondition.U2)) *
                                              _grid.Elements[boundaryCondition.ElemIndex].Functions[index[p]].ValueIn(_grid.Nodes[globalIndex1].Radius, phiJ) *
                                              _grid.Nodes[globalIndex1].Radius * _grid.Nodes[globalIndex1].Radius;
                    }

                    secondCondition[p] = boundaryCondition.Betta * sumOfInnerIntegral * _weights[j] / 2.0;
                }
            }

            return secondCondition;
        }


        public double[] CalcR(double elemLength, ThirdBoundaryCondition boundaryCondition)
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
                                              ((_grid.Elements[boundaryCondition.ElemIndex].Functions[boundaryCondition.FirstLocalIndex].ValueIn(rJ, _grid.Nodes[globalIndex1].Angle) * boundaryCondition.U1) + (
                                               _grid.Elements[boundaryCondition.ElemIndex].Functions[boundaryCondition.SecondLocalIndex].ValueIn(rJ, _grid.Nodes[globalIndex1].Angle) * boundaryCondition.U2)) *
                                              _grid.Elements[boundaryCondition.ElemIndex].Functions[index[p]].ValueIn(rJ, _grid.Nodes[globalIndex1].Angle) *
                                              rJ * rJ;
                    }

                    secondCondition[p] = boundaryCondition.Betta * sumOfInnerIntegral * _weights[j] / 2.0;
                }
            }

            return secondCondition;
        }

        private SymmetricMatrix GetHorizontalBounderMatrix(double elemLength, ThirdBoundaryCondition boundaryCondition)
        {
            double widthStep = elemLength / _numberOfSplits;
            var conditionMatrix = new SymmetricMatrix(new double[2, 2]);

            var globalIndex1 = _grid.Elements[boundaryCondition.ElemIndex]
                .NodeIndexes[boundaryCondition.FirstLocalIndex];

            var globalIndex2 = _grid.Elements[boundaryCondition.ElemIndex]
                .NodeIndexes[boundaryCondition.SecondLocalIndex];

            int[] index = new int[2] { boundaryCondition.FirstLocalIndex, boundaryCondition.SecondLocalIndex };

            for (int q = 0; q < 2; q++)
            {
                for (int p = q; p < 2; p++)
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
                                                  _grid.Elements[boundaryCondition.ElemIndex].Functions[index[p]]
                                                        .ValueIn(rJ,
                                                            _grid.Nodes[globalIndex1].Angle) *
                                                      _grid.Elements[boundaryCondition.ElemIndex].Functions[index[q]]
                                                          .ValueIn(rJ,
                                                              _grid.Nodes[globalIndex1].Angle) *
                                                  rJ * rJ;
                        }

                        conditionMatrix[p, q] = boundaryCondition.Betta * sumOfInnerIntegral * _weights[j] / 2.0;
                    }
                }
            }

            return conditionMatrix;
        }

        private SymmetricMatrix GetVerticalBounderMatrix(double elemLength, ThirdBoundaryCondition boundaryCondition)
        {
            double heightStep = elemLength / _numberOfSplits;
            var conditionMatrix = new SymmetricMatrix(new double[2, 2]);

            var globalIndex1 = _grid.Elements[boundaryCondition.ElemIndex]
                .NodeIndexes[boundaryCondition.FirstLocalIndex];

            var globalIndex2 = _grid.Elements[boundaryCondition.ElemIndex]
                .NodeIndexes[boundaryCondition.SecondLocalIndex];

            int[] index = new int[2] { boundaryCondition.FirstLocalIndex, boundaryCondition.SecondLocalIndex };

            for (int q = 0; q < 2; q++)
            {
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
                                                  _grid.Elements[boundaryCondition.ElemIndex].Functions[index[p]].ValueIn(_grid.Nodes[globalIndex1].Radius, phiJ) *
                                                      _grid.Elements[boundaryCondition.ElemIndex].Functions[index[q]].ValueIn(_grid.Nodes[globalIndex1].Radius, phiJ) *
                                                  _grid.Nodes[globalIndex1].Radius * _grid.Nodes[globalIndex1].Radius;
                        }

                        conditionMatrix[p, q] = boundaryCondition.Betta * sumOfInnerIntegral * _weights[j] / 2.0;
                    }
                }
            }

            return conditionMatrix;
        }
    }
}
