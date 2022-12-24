using CourseProject.DataStructures.BoundaryCondition;
using CourseProject.DataStructures;
using FileGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.DataStructures.BoundaryConditions;

namespace CourseProject.Readers.BoundaryConditions
{
    public class ThirdBoundaryConditionsReader : IReader<ThirdBoundaryCondition>
    {
        private Grid _grid;
        public ThirdBoundaryConditionsReader(Grid grid) { _grid = grid; }
        public override List<ThirdBoundaryCondition> Read()
        {
            List<ThirdBoundaryCondition> result = new List<ThirdBoundaryCondition>();
            using (StreamReader reader = new StreamReader(Config.RootPath + Config.ThirdBoundaryConditions))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();

                    result.Add(new ThirdBoundaryCondition(int.Parse(elemArray[0]), int.Parse(elemArray[1]), int.Parse(elemArray[2]), double.Parse(elemArray[3]), double.Parse(elemArray[4]), double.Parse(elemArray[5])));
                }
            }

            return result;
        }
    }
}
