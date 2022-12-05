using FileGenerators.DataStructures;
using System.Drawing;

namespace FileGenerators
{
    public static class Config
    {
        public const string RootPath = "../../../../Input/";
        public const string ElemFileName = "Elem.txt";
        public const string NodeFileName = "Node.txt";
        public const string MatFileName = "Mat.txt";

        public const int NumOfX = 3;
        public const int NumOfY = 2;

        public static RectangleLocation Location = new RectangleLocation(new PointF(0,0), new PointF(10,90));

        public const int NumOfMaterials = 10;
    }
}
