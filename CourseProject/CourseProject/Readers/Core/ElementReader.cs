using CourseProject.DataStructures;
using FileGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseProject.Readers.Core
{
    public class ElementReader : IReader<Element>
    {
        IReader<double> _materialReader;
        List<double> materials;
        public ElementReader(IReader<double> materialReader)
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

        private double GetMaterialByIndex(int index)
        {
            return materials[index];
        }
    }
}
