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

        public const int NumOfX = 3;
        public const int NumOfY = 2;

        public static RectangleLocation Location = new RectangleLocation(new Point(0,0), new Point(10,90));

        public const int NumOfMaterials = 10;

        public const int NumOfPoints = 3;

        public static Func<double, double, double> f = (r, phi) => r * r + phi * phi;
    }
}
