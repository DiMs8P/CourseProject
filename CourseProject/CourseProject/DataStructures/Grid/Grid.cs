using CourseProject.Calculus.LocalElements;
using CourseProject.Calculus.LocalElements.Fillers;
using CourseProject.Calculus.NumericalIntegration;
using CourseProject.DataStructures.Matrixes;
using FileGenerators;

namespace CourseProject.DataStructures
{
    public class Grid
    {
        public List<Element> Elements;
        public List<Node> Nodes;
        public double ElemWidth = 0;
        public double ElemHeight = 0;

        public Grid(List<Element> elements, List<Node> nodes, double elemWidth, double elemHeight)
        {
            Elements = elements;
            Nodes = nodes;
            ElemWidth = elemWidth;
            ElemHeight = elemHeight;

            BasicFunctionsCoefficientsGenerator generator = new BasicFunctionsCoefficientsGenerator(this);
            generator.Generate();

            LocalMatrixesFiller matrixFiller = new LocalMatrixesFiller(this);
            matrixFiller.Fill();

            LocalVectorFiller vectorFiller = new LocalVectorFiller(this);
            vectorFiller.Fill();

        }

    }
}

