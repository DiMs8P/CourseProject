using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGenerators.Generators
{
    public class MaterialGenerator : IGenerator
    {
        private int _numOfMaterials = 0;
        public MaterialGenerator(int numOfMaterials)
        {
            _numOfMaterials = numOfMaterials;
        }
        public void Generate(string rootPath) {

            using (StreamWriter writer = new StreamWriter(rootPath, false))
            {
                for(int i = 0; i < _numOfMaterials; i++)
                {
                    writer.WriteLine("0 0 0 0");
                }
            }
        }
    }
}

