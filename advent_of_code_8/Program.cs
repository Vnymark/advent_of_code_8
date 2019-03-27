using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_8
{
    class Program
    {
        static void Main(string[] args)
        {
            //var inputPath = @"../../../input.txt";
            var inputPath = @"../../../test.txt";
            string inputText = File.ReadAllText(inputPath);
            List<int> NumberList = inputText.Split(' ').Select(x => int.Parse(x)).ToList();

            List<int> ChildList = new List<int>();
            List<int> MetaDataList = new List<int>();
            int metaDataSum = 0;
            int MetaDataCount = 0;
            ParseHeader();

            while (NumberList.Count > 0)
            {
                if (ChildList.Last() != 0)
                {
                    ParseHeader();
                }
                else
                {
                    AddMetaData();
                    ChildList.RemoveAt((ChildList.Count()-1));
                }
            }

            void ParseHeader() {
                int i = 0;
                while (i < 2)
                {
                    int n = NumberList.First();
                    if (i == 0)
                    {
                        ChildList.Add(n);
                        NumberList.RemoveAt(0);
                    }
                    else
                    {
                        MetaDataList.Add(n);
                        NumberList.RemoveAt(0);
                    }
                    i++;
                }
            }


            void AddMetaData()
            {
                MetaDataCount = MetaDataList.Last();
                while (MetaDataCount > 0)
                {
                    metaDataSum += NumberList.First();
                    Console.WriteLine(NumberList.First());
                    NumberList.RemoveAt(0);
                    MetaDataCount--;
                }
                MetaDataList.RemoveAt(MetaDataList.Count - 1);
            }

            

            Console.ReadKey();

        }
    }
}
