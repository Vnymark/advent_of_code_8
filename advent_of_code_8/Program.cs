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
            //var inputPath = @"../../../input.txt";
            var inputPath = @"../../../test.txt";
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

            void ParseHeader() {
                node = new Node();
                int i = 0;
                while (i < 2)
                {
                    int n = NumberList.First();
                    if (i == 0)
                    {
                        node.NumberOfChildren = n;
                        ChildList.Add(n);
                        NumberList.RemoveAt(0);
                    }
                    else
                    {
                        node.NumberOfMetadata = n;
                        MetaDataList.Add(n);
                        NumberList.RemoveAt(0);
                    }
                    i++;
                }
                node.RootLevel = rootLevelCount;
                node.AddChild();
                NodeList.Add(node);
                rootLevelCount++;
            }

            void AddMetaData()
            {
                rootLevelCount--;
                Node currentNode = GetCurrentNode();
                metaDataCount = MetaDataList.Last();
                while (metaDataCount > 0)
                {
                    currentNode.MetaData.Add(NumberList.First());
                    metaDataSum += NumberList.First();
                    NumberList.RemoveAt(0);
                    metaDataCount--;
                }
                MetaDataList.RemoveAt(MetaDataList.Count - 1);
                
            }

            Node GetCurrentNode()
            {
                Node currentNode = NodeList.FindLast(x => x.RootLevel == (rootLevelCount));
                if (currentNode.MetaData == null)
                {
                    currentNode.MetaData = new List<int>();
                }
                return currentNode;
            }
            Console.WriteLine(metaDataSum);
            Console.ReadKey();
        }
    }
}
