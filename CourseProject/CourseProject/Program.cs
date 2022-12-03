using FileGenerators.Generators;
using FileGenerators;

namespace CourseProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            IGenerator generator = new ElementGenerator(10,10);
            generator.Generate(Config.RootPath + "Elem.txt");
        }
    }
}