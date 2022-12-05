using FileGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataStructures.Readers
{
    public class MaterialReader : IReader<float>
    {
        public override List<float> Read()
        {
            List<float> materials = new List<float>();
            using (StreamReader reader = new StreamReader(Config.RootPath + Config.MatFileName))
            {
                while (!reader.EndOfStream)
                {
                    var material = reader.ReadLine();
                    materials.Add(float.Parse(material));
                }
            }
            return materials;
        }
    }
}
