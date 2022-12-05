using FileGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseProject.DataStructures.Readers
{
    public class ElementReader : IReader<Element>
    {
        IReader<float> _materialReader;
        List<float> materials;
        public ElementReader(IReader<float> materialReader)
        {
            _materialReader = materialReader;
            materials = _materialReader.Read();
        }

        public override List<Element> Read()
        {
            List<Element> elements = new List<Element>();
            using (StreamReader reader = new StreamReader(Config.RootPath + Config.ElemFileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();

                    int materialIndex = int.Parse(elemArray[3]);
                    var material = GetMaterialByIndex(materialIndex);

                    elements.Add(
                        new Element(
                            new int[]
                            { int.Parse(elemArray[0]),
                            int.Parse(elemArray[1]),
                            int.Parse(elemArray[2]),
                            },
                            material));
                }
            }
            return elements;
        }

        private float GetMaterialByIndex(int index)
        {
            return materials[index];
        }
    }
}
