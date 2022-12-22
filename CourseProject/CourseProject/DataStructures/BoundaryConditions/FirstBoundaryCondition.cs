using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataStructures.BoundaryConditions
{
    public readonly record struct FirstBoundaryCondition(int FirstGlobalIndex, int SecondGlobalIndex, double FunctionValue);
}
