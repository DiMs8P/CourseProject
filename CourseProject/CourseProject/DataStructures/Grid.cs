using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataStructures
{
    public class Grid
    {
        Element[] _elements;
        Node[] _nodes;

        readonly float ElemWigth;
        readonly float ElemHeight;

        Grid(Element[] elements, Node[] nodes, float elemWigth, float elemHeight)
        {
            ElemWigth = elemWigth;
            ElemHeight = elemHeight;
            _elements = elements;
            _nodes = nodes;
        }

    }
}
