using System.Drawing;
using FileGenerators.DataStructures;
using Point = FileGenerators.DataStructures.Point;

namespace FileGenerators
{
    public static class Config
    {
        public const string RootPath = "../../../../Input/";
        public const string ElemFileName = "Elem.txt";
        public const string NodeFileName = "Node.txt";
        public const string MatFileName = "Mat.txt";
        public const string FirstBoundaryConditions = "FirstBoundaryConditions.txt";
        public const string SecondBoundaryConditions = "SecondBoundaryConditions.txt";
        public const string ThirdBoundaryConditions = "ThirdBoundaryConditions.txt";

        public const int Splitting = 1;
        public const int NumOfX = 1 * Splitting;
        public const int NumOfY = 1 * Splitting;


        public static RectangleLocation Location = new RectangleLocation(new Point(1, 1), new Point(3, 3));

        public const int NumOfMaterials = 10;

        public static Func<double, double, double> f = (r, phi) => ((-1 / r) + r);

        public static Func<double, double, double> lambda = (r, phi) => 1;
    }
}
