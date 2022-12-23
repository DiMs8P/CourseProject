using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Calculus.NumericalIntegration;
using CourseProject.DataStructures;
using CourseProject.DataStructures.Matrixes;
using FileGenerators;

namespace CourseProject.Calculus.LocalElements.Fillers
{
    internal class LocalMatrixesFiller
    {
        private readonly Grid _grid;
        private readonly LambdaDecomposition _lambdaDelegate;
        private double[] _rootsLegendrePolynomial = new double[] { -0.5773503, 0.5773503 };
        private double[] _weights = new double[] { 1.0, 1.0 };
        private readonly int _gaussSplitsNumber = 2;
        private readonly int _numberOfSplits = 512;
        private double _heightStep;
        private double _widthStep;

        public LocalMatrixesFiller(Grid grid)
        {
            _grid = grid;
            _lambdaDelegate = new LambdaDecomposition(_grid);
            _heightStep = _grid.ElemHeight / _numberOfSplits;
            _widthStep = _grid.ElemWidth / _numberOfSplits;
        }

        public void Fill()
        {
            GenerateMassMatrixes();
            GenerateStiffnessMatrixes();
        }

        private void GenerateMassMatrixes()
        {
            for (int evenElemIndex = 0; evenElemIndex < _grid.Elements.Count; evenElemIndex += 2)
            {
                _grid.Elements[evenElemIndex].MassMatrix = GetEvenMassMatrix(evenElemIndex) / _grid.Elements[evenElemIndex].Gamma;
            }

            for (int oddElemIndex = 1; oddElemIndex < _grid.Elements.Count; oddElemIndex += 2)
            {
                _grid.Elements[oddElemIndex].MassMatrix = GetOddMassMatrix(oddElemIndex) / _grid.Elements[oddElemIndex].Gamma;
            }
        }

        private void GenerateStiffnessMatrixes()
        {
            for (int evenElemIndex = 0; evenElemIndex < _grid.Elements.Count; evenElemIndex += 2)
            {
                _grid.Elements[evenElemIndex].StiffnessMatrix = GetEvenStiffnessMatrix(evenElemIndex);
            }

            for (int oddElemIndex = 1; oddElemIndex < _grid.Elements.Count; oddElemIndex += 2)
            {
                _grid.Elements[oddElemIndex].StiffnessMatrix = GetOddStiffnessMatrix(oddElemIndex);
            }
        }

        private SymmetricMatrix GetEvenStiffnessMatrix(int elemIndex)
        {
            var stiffnessMatrix = new SymmetricMatrix(new double[3, 3]);

            var lambdaForElem = _lambdaDelegate.In(_grid.Elements[elemIndex]);

            for (int q = 0; q < stiffnessMatrix.Data.GetLength(0); q++)
            {
                for (int p = q; p < stiffnessMatrix.Data.GetLength(1); p++)
                {
                    var outerIntegralValue = 0.0d;

                    for (int i = 0; i < _gaussSplitsNumber; i++)
                    {
                        double sumOfOuterIntergal = 0.0d;

                        for (int r = 0; r < _numberOfSplits; r++)
                        {
                            var rI = (_grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius + r * _widthStep +
                                      _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius +
                                      (r + 1) * _widthStep) / 2.0 + _rootsLegendrePolynomial[i] * _widthStep / 2;

                            var innerIntergalValue = 0.0d;

                            for (int j = 0; j < _gaussSplitsNumber; j++)
                            {
                                var sumOfInnerIntegral = 0.0d;
                                for (int phi = 0; phi < _numberOfSplits - r; phi++)
                                {
                                    var phiJ = (_grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle +
                                                phi * _heightStep +
                                                _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle +
                                                (phi + 1) * _heightStep) / 2.0d +
                                               _rootsLegendrePolynomial[j] * _heightStep / 2;

                                    sumOfInnerIntegral +=
                                        rI * _heightStep *
                                        (_grid.Elements[elemIndex].Functions[p].ValueRadiusDerivativeIn(rI, phiJ) *
                                         _grid.Elements[elemIndex].Functions[q].ValueRadiusDerivativeIn(rI, phiJ) +
                                         _grid.Elements[elemIndex].Functions[p].ValueAngleDerivativeIn(rI, phiJ) *
                                          _grid.Elements[elemIndex].Functions[q].ValueAngleDerivativeIn(rI, phiJ));
                                        // /
                                        // (rI * rI)) *
                                        //lambdaForElem(rI, phiJ);
                                }

                                innerIntergalValue += sumOfInnerIntegral * _weights[j] / 2.0;
                            }

                            sumOfOuterIntergal += _widthStep * innerIntergalValue;
                        }

                        outerIntegralValue += _weights[i] / 2.0d * sumOfOuterIntergal;
                    }

                    stiffnessMatrix.Data[p, q] = outerIntegralValue;
                }
            }

            return stiffnessMatrix;
        }

        private SymmetricMatrix GetOddStiffnessMatrix(int elemIndex)
        {
            var stiffnessMatrix = new SymmetricMatrix(new double[3, 3]);

            var lambdaForElem = _lambdaDelegate.In(_grid.Elements[elemIndex]);

            for (int q = 0; q < stiffnessMatrix.Data.GetLength(0); q++)
            {
                for (int p = q; p < stiffnessMatrix.Data.GetLength(1); p++)
                {
                    var outerIntegralValue = 0.0d;

                    for (int i = 0; i < _gaussSplitsNumber; i++)
                    {
                        double sumOfOuterIntergal = 0.0d;

                        for (int r = 0; r < _numberOfSplits; r++)
                        {
                            var rI = (_grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius + r * _widthStep +
                                      _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius +
                                      (r + 1) * _widthStep) / 2.0 + _rootsLegendrePolynomial[i] * _widthStep / 2;

                            var innerIntergalValue = 0.0d;

                            for (int j = 0; j < _gaussSplitsNumber; j++)
                            {
                                var sumOfInnerIntegral = 0.0d;
                                for (int phi = _numberOfSplits - r; phi < _numberOfSplits; phi++)
                                {
                                    var phiJ = (_grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle +
                                                phi * _heightStep +
                                                _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle +
                                                (phi + 1) * _heightStep) / 2.0d +
                                               _rootsLegendrePolynomial[j] * _heightStep / 2;

                                    sumOfInnerIntegral +=
                                        rI * _heightStep *
                                        (_grid.Elements[elemIndex].Functions[p].ValueRadiusDerivativeIn(rI, phiJ) *
                                         _grid.Elements[elemIndex].Functions[q].ValueRadiusDerivativeIn(rI, phiJ) +
                                         _grid.Elements[elemIndex].Functions[p].ValueAngleDerivativeIn(rI, phiJ) *
                                          _grid.Elements[elemIndex].Functions[q].ValueAngleDerivativeIn(rI, phiJ));
                                        // /
                                        // (rI * rI)) *
                                        //lambdaForElem(rI, phiJ);
                                }

                                innerIntergalValue += sumOfInnerIntegral * _weights[j] / 2.0;
                            }

                            sumOfOuterIntergal += _widthStep * innerIntergalValue;
                        }

                        outerIntegralValue += _weights[i] / 2.0d * sumOfOuterIntergal;
                    }

                    stiffnessMatrix.Data[p, q] = outerIntegralValue;
                }
            }

            return stiffnessMatrix;
        }

        private SymmetricMatrix GetEvenMassMatrix(int elemIndex)
        {
            var massMatrix = new SymmetricMatrix(new double[3, 3]);

            for (int q = 0; q < massMatrix.Data.GetLength(0); q++)
            {
                for (int p = q; p < massMatrix.Data.GetLength(1); p++)
                {
                    var outerIntegralValue = 0.0d;

                    for (int i = 0; i < _gaussSplitsNumber; i++)
                    {
                        double sumOfOuterIntergal = 0.0d;

                        for (int r = 0; r < _numberOfSplits; r++)
                        {
                            var rI = (_grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius + r * _widthStep +
                                      _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius +
                                      (r + 1) * _widthStep) / 2.0 + _rootsLegendrePolynomial[i] * _widthStep / 2;

                            var innerIntergalValue = 0.0d;

                            for (int j = 0; j < _gaussSplitsNumber; j++)
                            {
                                var sumOfInnerIntegral = 0.0d;
                                for (int phi = 0; phi < _numberOfSplits - r; phi++)
                                {
                                    var phiJ = (_grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle +
                                                phi * _heightStep +
                                                _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle +
                                                (phi + 1) * _heightStep) / 2.0d +
                                               _rootsLegendrePolynomial[j] * _heightStep / 2;

                                    sumOfInnerIntegral += rI * _heightStep *
                                                          (_grid.Elements[elemIndex].Functions[p].ValueIn(rI, phiJ) *
                                                           _grid.Elements[elemIndex].Functions[q].ValueIn(rI, phiJ));
                                }

                                innerIntergalValue += sumOfInnerIntegral * _weights[j] / 2.0;
                            }

                            sumOfOuterIntergal += _widthStep * innerIntergalValue;
                        }

                        outerIntegralValue += _weights[i] / 2.0d * sumOfOuterIntergal;
                    }

                    massMatrix.Data[p, q] = outerIntegralValue;
                }
            }

            return massMatrix;
        }

        private SymmetricMatrix GetOddMassMatrix(int elemIndex)
        {
            var massMatrix = new SymmetricMatrix(new double[3, 3]);

            for (int q = 0; q < massMatrix.Data.GetLength(0); q++)
            {
                for (int p = q; p < massMatrix.Data.GetLength(1); p++)
                {
                    var outerIntegralValue = 0.0d;

                    for (int i = 0; i < _gaussSplitsNumber; i++)
                    {
                        double sumOfOuterIntergal = 0.0d;

                        for (int r = 0; r < _numberOfSplits; r++)
                        {
                            var rI = (_grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius + r * _widthStep +
                                      _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Radius +
                                      (r + 1) * _widthStep) / 2.0 + _rootsLegendrePolynomial[i] * _widthStep / 2.0;

                            var innerIntergalValue = 0.0d;

                            for (int j = 0; j < _gaussSplitsNumber; j++)
                            {
                                var sumOfInnerIntegral = 0.0d;
                                for (int phi = _numberOfSplits - r; phi < _numberOfSplits; phi++)
                                {
                                    var phiJ = (_grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle +
                                                phi * _heightStep +
                                                _grid.Nodes[_grid.Elements[elemIndex].NodeIndexes[0]].Angle +
                                                (phi + 1) * _heightStep) / 2.0d +
                                               _rootsLegendrePolynomial[j] * _heightStep / 2;

                                    sumOfInnerIntegral += rI * _heightStep *
                                                          (_grid.Elements[elemIndex].Functions[p].ValueIn(rI, phiJ) *
                                                           _grid.Elements[elemIndex].Functions[q].ValueIn(rI, phiJ));
                                }

                                innerIntergalValue += sumOfInnerIntegral * _weights[j] / 2.0;
                            }

                            sumOfOuterIntergal += _widthStep * innerIntergalValue;
                        }

                        outerIntegralValue += _weights[i] / 2.0d * sumOfOuterIntergal;
                    }

                    massMatrix.Data[p, q] = outerIntegralValue;
                }
            }

            return massMatrix;
        }
    }
}