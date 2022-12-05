using CourseProject.DataStructures.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataStructures.Factories
{
    public class GridFactory : IGridFactory
    {
        IReader<Node> _nodeReader = new NodeReader();
        IReader<Element> _elementReader;
        IReader<float> _materialReader;

        public GridFactory()
        {
            _materialReader = new MaterialReader();
            _elementReader = new ElementReader(_materialReader); 
        }
        public Grid CreateGrid()
        {
            var elements = _elementReader.Read();
            var nodes = _nodeReader.Read();
            if (elements == null || nodes == null || nodes.Count < 3)
            {
                throw new InvalidDataException();
            }

            return new Grid(elements, nodes, nodes[1].Radius - nodes[0].Radius, nodes[2].Angle - nodes[0].Angle);
        }

    }
}
