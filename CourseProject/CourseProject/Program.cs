using FileGenerators.Generators;
using FileGenerators;
using System.Drawing;

namespace CourseProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var gridGenerator = new GridGenerator();
            gridGenerator.Generate();
        }
    }
}