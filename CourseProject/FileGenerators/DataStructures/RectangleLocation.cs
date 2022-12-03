using System.Drawing;

namespace FileGenerators.DataStructures
{
    public readonly record struct RectangleLocation(PointF LowerLeft, PointF UpperRight);
}
