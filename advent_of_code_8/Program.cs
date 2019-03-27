using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_8
{
    class Program
    {
        public static List<Node> NodeList;
        static void Main(string[] args)
        {
            var inputPath = @"../../../input.txt";
            //var inputPath = @"../../../test.txt";
            string inputText = File.ReadAllText(inputPath);
            List<int> NumberList = inputText.Split(' ').Select(x => int.Parse(x)).ToList();

            List<int> ChildList = new List<int>();
            List<int> MetaDataList = new List<int>();
            NodeList = new List<Node>();
            Node node = new Node();
            int metaDataSum = 0;
            int metaDataCount = 0;
            int rootLevelCount = 0;
            ParseHeader();

            //Run through License File
            //If a child exist, add that child.
            //Else, add the metadata.
            while (NumberList.Count > 0)
            {
                int childCount = ChildList.Last();
                ChildList.RemoveAt((ChildList.Count() - 1));
                if (childCount != 0)
                {
                    childCount = childCount - 1;
                    ChildList.Add(childCount);
                    ParseHeader();
                }
                else
                {
                    AddMetaData();
                }
            }

            //Part 2
            //Reverse NodeList to calculate children before their parent.
            List<Node> ReversedNodeList = Program.NodeList;
            ReversedNodeList.Reverse();
            foreach (Node n in Program.NodeList)
            {
                n.CalculateChildSum();
            }

            //Parsing the header and adding the newly parsed Node as a child to the correct Node parent.
            void ParseHeader() {
                node = new Node();
                int i = 0;
                while (i < 2)
                {
                    if (i == 0)
                    {
                        node.NumberOfChildren = NumberList.First();
                        ChildList.Add(NumberList.First());
                        NumberList.RemoveAt(0);
                    }
                    else
                    {
                        node.NumberOfMetadata = NumberList.First();
                        MetaDataList.Add(NumberList.First());
                        NumberList.RemoveAt(0);
                    }
                    i++;
                }
                node.RootLevel = rootLevelCount;
                node.AddChild();
                NodeList.Add(node);
                rootLevelCount++;
            }

            //Add MetaData, both to metaDataSum for part1.
            //But also adding the metaData to Nodes for part 2.
            void AddMetaData()
            {
                rootLevelCount--;
                Node currentNode = GetCurrentNode();
                metaDataCount = MetaDataList.Last();
                while (metaDataCount > 0)
                {
                    currentNode.MetaData.Add(NumberList.First());
                    currentNode.MetaDataSum += NumberList.First();
                    metaDataSum += NumberList.First();
                    NumberList.RemoveAt(0);
                    metaDataCount--;
                }
                MetaDataList.RemoveAt(MetaDataList.Count - 1);
            }

            //Get the current Node (used while adding MetaData).
            Node GetCurrentNode()
            {
                Node currentNode = NodeList.FindLast(x => x.RootLevel == (rootLevelCount));
                if (currentNode.MetaData == null)
                {
                    currentNode.MetaData = new List<int>();
                }
                return currentNode;
            }

            //Print the result
            Console.WriteLine("The sum of the MetaData: {0}", metaDataSum);
            Console.WriteLine("The value of the root Node, based on its childrens value: {0}", ReversedNodeList.ElementAt(ReversedNodeList.Count-1).ChildSum);
            Console.ReadKey();
        }
    }
}
