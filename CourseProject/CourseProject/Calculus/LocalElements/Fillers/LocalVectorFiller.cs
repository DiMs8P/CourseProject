using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.DataStructures;
using CourseProject.DataStructures.Matrixes;
using FileGenerators;

namespace CourseProject.Calculus.LocalElements.Fillers
{
    public class LocalVectorFiller
    {
        private readonly Grid _grid;
        public LocalVectorFiller(Grid grid)
        {
            _grid = grid;
        }

        public void Fill()
        {
            FillLocalVectors();
        }
        private void FillLocalVectors()
        {
            foreach (var Elem in _grid.Elements)
            {
                List<double> func = new List<double>();
                foreach (var nodeIndex in Elem.NodeIndexes)
                {
                    func.Add(Config.f(_grid.Nodes[nodeIndex].Radius, _grid.Nodes[nodeIndex].Angle));
                }

                Elem.LocalVector = MultiplyMatrixByVector(Elem.MassMatrix / Elem.Gamma, func);
            }
        }

        double[] MultiplyMatrixByVector(SymmetricMatrix matrix, List<double> func)
        {
            if (matrix.Data.GetLength(0) != matrix.Data.GetLength(1) && matrix.Data.GetLength(0) != func.Count)
                throw new ArgumentException();

            double[] localVector = new double[func.Count];
            for (int i = 0; i < matrix.Data.GetLength(0); i++)
            {
                double result = 0;
                for (int j = 0; j < matrix.Data.GetLength(1); j++)
                {
                    result += matrix[i, j] * func[j];
                }

                localVector[i] = result;
            }
            return localVector; 
        }

    }
}
