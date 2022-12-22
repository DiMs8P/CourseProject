using System.Reflection.Emit;
using FileGenerators;
using FileGenerators.Generators;
using CourseProject.DataStructures;
using CourseProject.DataStructures.Readers;
using CourseProject.Readers;

namespace CourseProject.Tests
{
    public class ReaderTests
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

            Assert.AreEqual((Config.NumOfX + 1) * (Config.NumOfY + 1), nodes.Count);
        }

        [Test]
        public void ElementReaderTest()
        {
            var materiaReader = new MaterialReader();
            var reader = new ElementReader(materiaReader);
            var nodes = reader.Read();

            Assert.AreEqual(Config.NumOfX * Config.NumOfY * 2, nodes.Count);
        }
    }
}