using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileGenerators.DataStructures;

namespace FileGenerators.Generators
{
    internal class NodeGenerator : IGenerator
    {
        int _numOnX;
        int _numOnY;
        RectangleLocation _rectangleLocation;

        double _elementWidth;
        double _elementHeight;
        public NodeGenerator(RectangleLocation rectangleLocation, int numOnX, int numOnY)
        {
            _numOnX = numOnX;
            _numOnY = numOnY;
            _rectangleLocation = rectangleLocation;

            _elementWidth = (rectangleLocation.UpperRight.R - rectangleLocation.LowerLeft.R) / numOnX;
            _elementHeight = (rectangleLocation.UpperRight.Phi - rectangleLocation.LowerLeft.Phi) / numOnY;
        }

        public void Generate()
        {
            using (StreamWriter writer = new StreamWriter(Config.RootPath + Config.NodeFileName, false))
            {
                for (int j = 0; j < _numOnY + 1; j++)
                {
                    for (int i = 0; i < _numOnX + 1; i++)
                    {
                        writer.WriteLine(
                            Convert.ToString(_rectangleLocation.LowerLeft.R + i * _elementWidth) +
                            " " +
                            Convert.ToString(_rectangleLocation.LowerLeft.Phi + j * _elementHeight) + " " +
                            Convert.ToString(Config.lambda(_rectangleLocation.LowerLeft.R + i * _elementWidth, _rectangleLocation.LowerLeft.Phi + j * _elementHeight))
                            );
                    }
                }
            }
        }
    }
}
