using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.DataStructures.BoundaryCondition;
using CourseProject.DataStructures;
using CourseProject.DataStructures.BoundaryConditions;
using CourseProject.DataStructures.Matrixes;
using FileGenerators;

namespace CourseProject.Readers.BoundaryConditions
{
    public class FirstBoundaryConditionsReader : IReader<FirstBoundaryCondition>
    {
        private Grid _grid;
        public FirstBoundaryConditionsReader(Grid grid) { _grid = grid; }
        public override List<FirstBoundaryCondition> Read()
        {
            List<FirstBoundaryCondition> result = new List<FirstBoundaryCondition>();
            using (StreamReader reader = new StreamReader(Config.RootPath + Config.FirstBoundaryConditions))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').ToArray();

                    result.Add(new FirstBoundaryCondition(int.Parse(elemArray[0]), int.Parse(elemArray[1]), double.Parse(elemArray[2])));
                }
            }

            return result;
        }
    }
}
