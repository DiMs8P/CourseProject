using FileGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Readers.Core
{
    public class MaterialReader : IReader<double>
    {
        public override List<double> Read()
        {
            List<double> materials = new List<double>();
            using (StreamReader reader = new StreamReader(Config.RootPath + Config.MatFileName))
            {
                while (!reader.EndOfStream)
                {
                    var material = reader.ReadLine();
                    materials.Add(double.Parse(material));
                }
            }
            return materials;
        }
    }
}
