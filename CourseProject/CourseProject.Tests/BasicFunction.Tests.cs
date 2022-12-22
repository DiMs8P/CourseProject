using CourseProject.DataStructures.Factories;
using CourseProject.DataStructures.Matrixes;
using FileGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Calculus.LocalElements.BasicFunctions;

namespace CourseProject.Tests
{
    internal class BasicFunctionTest
    {
        private Matrix _D;
        [SetUp]
        public void Setup()
        {
            GridGenerator generator = new GridGenerator();
            generator.Generate();
        }

        [Test]
        public void PortraitTest()
        {
            var factory = new GridFactory();
            var Grid = factory.CreateGrid();
            var function = Grid.Elements[0].Functions[0];
            BasicFunction expectedFunction = new BasicFunction(new double[] {-225, 0, 25});
            Assert.AreEqual(expectedFunction, function);

        }
    }
}
