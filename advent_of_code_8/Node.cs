using System;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_8
{
    class Node
    {
        public int NumberOfChildren { get; set; }
        public int NumberOfMetadata { get; set; }
        public List<Node> Children { get; set; }
        public List<int> MetaData { get; set; }
        public int RootLevel { get; set; }

        public void AddChild()
        {
            if (this.RootLevel > 0)
            {
                
                Node parentNode = Program.NodeList.FindLast(x => x.RootLevel == (this.RootLevel - 1));
                if (parentNode != null)
                {
                    if(parentNode.Children == null)
                    {
                        parentNode.Children = new List<Node>();
                    }
                    parentNode.Children.Add(this); 
                }
            }
        }

        public void CalculateSum()
        {

        }
    }
}
