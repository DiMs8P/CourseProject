using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Calculus.LocalElements;
using CourseProject.Readers;
using CourseProject.Readers.Core;

namespace CourseProject.DataStructures.Factories
{
    public class GridFactory : IGridFactory
    {
        IReader<Node> _nodeReader = new NodeReader();
        IReader<Element> _elementsReader;
        IReader<double> _materialReader;

        public GridFactory()
        {
            _materialReader = new MaterialReader();
            _elementsReader = new ElementReader(_materialReader); 
        }
        public Grid CreateGrid()
        {
            var elements = _elementsReader.Read();

            var nodes = _nodeReader.Read();

            if (elements == null || nodes == null || nodes.Count < 3)
            {
                throw new InvalidDataException();
            }

            return new Grid(elements, nodes, Math.Abs(nodes[elements[0].NodeIndexes[1]].Radius - nodes[elements[0].NodeIndexes[0]].Radius), Math.Abs(nodes[elements[0].NodeIndexes[2]].Angle - nodes[elements[0].NodeIndexes[0]].Angle));
        }

    }
}
