﻿using CourseProject.DataStructures;
using FileGenerators;

namespace CourseProject.Readers.Core
{
    public class NodeReader : IReader<Node>
    {


        public override List<Node> Read()
        {
            List<Node> nodes = new List<Node>();
            using (StreamReader reader = new StreamReader(Config.RootPath + Config.NodeFileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elemArray = line.Split(' ').Select(x => Convert.ToDouble(x)).ToArray();
                    nodes.Add(new Node(elemArray[0], elemArray[1], elemArray[2]));
                }
            }
            return nodes;
        }
    }
}
