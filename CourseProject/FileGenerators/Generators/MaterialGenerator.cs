using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGenerators.Generators
{
    internal class MaterialGenerator : IGenerator
    {
        private int _numOfMaterials = 0;
        public MaterialGenerator(int numOfMaterials)
        {
            _numOfMaterials = numOfMaterials;
        }
        public void Generate() {

            using (StreamWriter writer = new StreamWriter(Config.RootPath + Config.MatFileName, false))
            {
                for(int i = 0; i < _numOfMaterials; i++)
                {
                    writer.WriteLine("1");
                }
            }
        }
    }
}

