using System.Reflection.Emit;
using FileGenerators;
using FileGenerators.Generators;
using CourseProject.DataStructures;
using CourseProject.DataStructures.Readers;
namespace CourseProject.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NodesReaderTest()
        {
            IReader<Node> reader = new NodeReader();
            var nodes = reader.Read();

            Assert.AreEqual(25, nodes.Count);
        }

        [Test]
        public void ElementReaderTest()
        {
            var materiaReader = new MaterialReader();
            var reader = new ElementReader(materiaReader);
            var nodes = reader.Read();

            Assert.AreEqual(200, nodes.Count);
        }
    }
}