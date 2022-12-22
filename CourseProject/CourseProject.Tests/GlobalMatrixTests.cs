using System.Reflection.Emit;
using FileGenerators;
using FileGenerators.Generators;
using CourseProject.DataStructures;
using CourseProject.DataStructures.Factories;
using CourseProject.DataStructures.Matrixes;

namespace CourseProject.Tests
{
    public class GlobalMatrixTests
    {
        [SetUp]
        public void Setup()
        {
            GridGenerator generator = new GridGenerator();
            generator.Generate();
        }

        [Test]
        public void PortraitTest()
        {
            var matrix = new GlobalMatrix(new GridFactory());

            Assert.AreEqual(Config.NumOfY * Config.NumOfX * 2, matrix.RowPtr.Length);
        }
    }
}