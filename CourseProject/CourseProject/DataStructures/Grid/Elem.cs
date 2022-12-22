using CourseProject.DataStructures.Matrixes;
using System.Xml.Linq;
using CourseProject.Calculus.LocalElements;
using CourseProject.Calculus.LocalElements.BasicFunctions;

namespace CourseProject.DataStructures
{
    public class Element
    {
        public SymmetricMatrix MassMatrix;
        public SymmetricMatrix StiffnessMatrix;
        public double[] LocalVector;
        public int[] NodeIndexes;
        public double Gamma;
        public List<BasicFunction> Functions = new List<BasicFunction>(3);

        public Element(int[] nodeIndexes, double gamma)
        {
            Gamma = gamma;
            NodeIndexes = nodeIndexes;

        }

        public void SetFunctions(List<BasicFunction> functions)
        {
            if (functions.Count < 3)
            {
                throw new ArgumentException();
            }
            Functions = functions;
        }

        public void SetMassMatrix(SymmetricMatrix matrix)
        {
            MassMatrix = matrix;
        }

        public void SetStiffnessMatrix(SymmetricMatrix matrix)
        {
            StiffnessMatrix = matrix;
        }
    }

}

