using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileGenerators.DataStructures;

namespace FileGenerators.Generators
{
    public class NodeGenerator : IGenerator
    {
        int _numOnX;
        int _numOnY;
        RectangleLocation _rectangleLocation;

        float _elementWidth;
        float _elementHeight;
        public NodeGenerator(RectangleLocation rectangleLocation, int numOnX, int numOnY)
        {
            _numOnX = numOnX;
            _numOnY = numOnY;
            _rectangleLocation = rectangleLocation;

            _elementWidth = (rectangleLocation.UpperRight.X - rectangleLocation.LowerLeft.X) / numOnX;
            _elementHeight = (rectangleLocation.UpperRight.Y - rectangleLocation.LowerLeft.Y) / numOnY;
        }

        public void Generate(string rootPath)
        {
            using (StreamWriter writer = new StreamWriter(rootPath, false))
            {
                for (int j = 0; j < _numOnY + 1; j++)
                {
                    for (int i = 0; i < _numOnX + 1; i++)
                    {
                        writer.WriteLine(
                            Convert.ToString(_rectangleLocation.LowerLeft.X + i * _elementWidth) +
                            " " +
                            Convert.ToString(_rectangleLocation.LowerLeft.Y + j * _elementHeight) +
                            " 0"
                            );
                    }
                }
            }
        }
    }
}
