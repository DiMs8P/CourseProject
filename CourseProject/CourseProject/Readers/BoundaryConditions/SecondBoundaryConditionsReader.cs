using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.DataStructures;
using CourseProject.DataStructures.BoundaryCondition;
using FileGenerators;

namespace CourseProject.Readers.BoundaryConditions
{
    public class SecondBoundaryConditionsReader : IReader<SecondBoundaryCondition>
    {
        private Grid _grid;
        public SecondBoundaryConditionsReader(Grid grid) { _grid = grid; }
        public override List<SecondBoundaryCondition> Read()
        {
            List<SecondBoundaryCondition> result = new List<SecondBoundaryCondition>();
            using (StreamReader reader = new StreamReader(Config.RootPath + Config.SecondBoundaryConditions))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();

                    result.Add(new SecondBoundaryCondition(int.Parse(elemArray[0]), int.Parse(elemArray[1]), double.Parse(elemArray[2])));
                }
            }

            return result;
        }
    }
}
