using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.DataStructures;

namespace CourseProject.Calculus.NumericalIntegration
{
    public class LambdaDecomposition
    {
        private Grid _grid;
        public LambdaDecomposition(Grid grid)
        {
            _grid = grid;
        }

        public Func<double, double, double> In(Element elem)
        {
            return (r, phi) =>
            {
                double result = 0;
                for (int i = 0; i < elem.Functions.Count; i++)
                {
                    result += elem.Functions[i].ValueIn(r, phi) * _grid.Nodes[elem.NodeIndexes[i]].Diffusion;
                }

                return result;
            };
        }
    }
}
