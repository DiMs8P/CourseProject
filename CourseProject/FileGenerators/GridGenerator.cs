using FileGenerators.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGenerators
{
    //Facade
    public class GridGenerator
    {
        List<IGenerator> generators = new List<IGenerator>();
        public GridGenerator()
        {
            generators.Add(new ElementGenerator(Config.NumOfX, Config.NumOfY));
            generators.Add(new NodeGenerator(Config.Location, Config.NumOfX, Config.NumOfY));
            generators.Add(new MaterialGenerator(Config.NumOfMaterials));
        }
        public void Generate()
        {
            foreach(var generator in generators)
            {
                generator.Generate();
            }
        }
    }
}
