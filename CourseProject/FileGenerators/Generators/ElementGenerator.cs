using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGenerators.Generators
{
    public class ElementGenerator : IGenerator
    {
        int _numOnX;
        int _numOnY;
        public ElementGenerator(int numOnX, int numOnY)
        {
            _numOnX = numOnX;
            _numOnY = numOnY;
        }

        public void Generate(string rootPath)
        {
            using (StreamWriter writer = new StreamWriter(rootPath, false))
            {
                for (int i = 0; i < _numOnY; i++)
                {
                    for (int j = 0; j < _numOnX; j++)
                    {
                        GenerateElem(i, j, rootPath, writer);
                    }
                }
            }
        }

        private void GenerateElem(int i, int j, string rootPath, StreamWriter writer)
        {
            WriteLowerTriangle(i, j, writer);
            WriteUpperTriangle(i, j, writer);
        }

        public void WriteLowerTriangle(int i, int j, StreamWriter writer)
        {
            writer.WriteLine(
                   (j + (_numOnX + 1) * i).ToString() +
                   " " +
                   ((j + (_numOnX + 1) * i) + 1).ToString() +
                   " " +
                    (j + (_numOnX + 1) * (i + 1)).ToString() +
                    " " + 0
                   );
        }

        public void WriteUpperTriangle(int i, int j, StreamWriter writer)
        {
            writer.WriteLine(
                    ((j + (_numOnX + 1) * i) + 1).ToString() +
                    " " +
                    (j + (_numOnX + 1) * (i + 1)).ToString() +
                    " " +
                    ((j + (_numOnX + 1) * (i + 1)) + 1).ToString() +
                    " " + 0
                    );
        }
    }
}
