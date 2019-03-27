using System;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_8
{
    class Node
    {
        public int NumberOfChildren { get; set; }
        public int NumberOfMetadata { get; set; }
        public Node Parent { get; set; }
        public List<int> MetaData { get; set; }
        public int RootLevel { get; set; }

        public void AddParent()
        {
            if (this.RootLevel > 0)
            {
                //List<Node> Children = new List<Node>();
                Node parentNode = Program.NodeList.FindLast(x => x.RootLevel == (this.RootLevel - 1));
                if (parentNode != null)
                {
                    this.Parent = parentNode;
                }
                //Children.Add(this);
                //parentNode.Parent = Children;
            }
        }
    }
}
