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
    internal class FirstBoundaryConditionsApplyer
    {
        private int _numOfNodes = 2;
        private IReader<FirstBoundaryCondition> _reader;
        public FirstBoundaryConditionsApplyer(IReader<FirstBoundaryCondition> reader)
        {
            _reader = reader;
        }

        public void Apply(Grid grid, GlobalMatrix matrix, GlobalVector globalVector)
        {
            var allBoundatyConditions = _reader.Read();

            foreach (var boundaryCondition in allBoundatyConditions)
            {
                List<int> globalNodesIndexes = new List<int>() { boundaryCondition.FirstGlobalIndex, boundaryCondition.SecondGlobalIndex };

                for (int i = 0; i < globalNodesIndexes.Count; i++)
                {
                    foreach (var (columnIndex, value) in matrix.ColumnIndexValuesByRow(globalNodesIndexes[i]))
                    {
                        globalVector.Add(columnIndex, -boundaryCondition.FunctionValue * value);
                        matrix.SetValue(globalNodesIndexes[i], columnIndex, 0);
                    }

                    matrix.SetValue(globalNodesIndexes[i], globalNodesIndexes[i], 1);
                    globalVector.Set(globalNodesIndexes[i], boundaryCondition.FunctionValue);

                    for (int p = globalNodesIndexes[i] + 1; p < matrix.RowPtr.Length; p++)
                    {
                        foreach (var (columnIndex, value) in matrix.ColumnIndexValuesByRow(p))
                        {
                            if (columnIndex != globalNodesIndexes[i])
                                continue;

                            globalVector.Add(p, -boundaryCondition.FunctionValue * value);
                            matrix.SetValue(p, columnIndex, 0);
                        }
                    }

                }
            }
        }


    }
}
