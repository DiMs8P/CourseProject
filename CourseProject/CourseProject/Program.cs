using FileGenerators.Generators;
using FileGenerators;
using System.Drawing;
using CourseProject.BoundaryConditions;
using CourseProject.Calculus.LocalElements;
using CourseProject.DataStructures;
using CourseProject.DataStructures.Factories;
using CourseProject.Calculus.Global;
using CourseProject.DataStructures.BoundaryCondition;
using CourseProject.DataStructures.BoundaryConditions;
using CourseProject.Readers;
using CourseProject.Readers.BoundaryConditions;

namespace CourseProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            GridGenerator generator = new GridGenerator();
            generator.Generate();


            IGridFactory factory = new GridFactory();
            Grid grid = factory.CreateGrid();

            GlobalMatrix matrix = new GlobalMatrix(grid);
            GlobalVector vector = new GlobalVector(grid);
            vector.Init();

            IReader<SecondBoundaryCondition> reader2 = new SecondBoundaryConditionsReader(grid);
            SecondBoundaryConditionsApplyer applyer2 = new SecondBoundaryConditionsApplyer(reader2);

            IReader<ThirdBoundaryCondition> reader3 = new ThirdBoundaryConditionsReader(grid);
            ThirdBoundaryConditionsApplyer applyer3 = new ThirdBoundaryConditionsApplyer(reader3);

            IReader<FirstBoundaryCondition> reader1 = new FirstBoundaryConditionsReader(grid);
            FirstBoundaryConditionsApplyer applyer1 = new FirstBoundaryConditionsApplyer(reader1);

            applyer2.Apply(grid, vector);
            applyer1.Apply(grid, matrix, vector);

            applyer3.ApplyMatrix(grid, matrix);
            applyer3.ApplyVector(grid, vector);
        }
    }
}