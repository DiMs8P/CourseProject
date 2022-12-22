using CourseProject.Calculus.Global;
using CourseProject.DataStructures;
using CourseProject.DataStructures.BoundaryCondition;
using CourseProject.Readers;

namespace CourseProject.BoundaryConditions
{
    internal class SecondBoundaryConditionsApplyer
    {
        private int _numOfNodes = 2;
        private IReader<SecondBoundaryCondition> _reader;
        public SecondBoundaryConditionsApplyer(IReader<SecondBoundaryCondition> reader)
        {
            _reader = reader;
        }

        public void Apply(Grid grid, GlobalVector globalVector)
        {
            var allBoundatyConditions = _reader.Read();

            foreach (var boundaryCondition in allBoundatyConditions)
            {
                bool bounderIsHorizontal =
                    (boundaryCondition.SecondGlobalIndex - boundaryCondition.FirstGlobalIndex) == 1;

                var elemLength = bounderIsHorizontal ? grid.ElemWidth : grid.ElemHeight;

                globalVector.Add(boundaryCondition.FirstGlobalIndex, elemLength * boundaryCondition.Theta / 2);
                globalVector.Add(boundaryCondition.SecondGlobalIndex, elemLength * boundaryCondition.Theta / 2);
            }
        }
    }
}
