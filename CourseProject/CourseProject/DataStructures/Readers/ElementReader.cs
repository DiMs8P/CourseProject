using FileGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataStructures.Readers
{
    public class ElementReader : IReader<Element>
    {
        public override List<Element> Read()
        {
            List<Element> nodes = new List<Element>();
            using (StreamReader reader = new StreamReader(Config.RootPath + Config.NodeFileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();

                    int materialIndex = (int)elemArray[3];
                    var material = GetMaterialByIndex(materialIndex);
                }
            }
            return nodes;
        }

        private float GetMaterialByIndex(int index)
        {
            return 1.0f;
        }
    }
}
