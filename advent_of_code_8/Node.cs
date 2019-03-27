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
        public int MetaDataSum { get; set; }
        public int ChildSum { get; set; }

        //Takes the current Node and adds to the last added Node, that has a rootlevel of one higher(lower in number).
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
        
        //If Node has children, check if the index of those children(starting at 1), matches any of the metadata numbers.
        //If the matching children have children of their own, add their ChildSum.
        //Else, use their MetaDataSum.
        public void CalculateChildSum()
        {
            if (this.Children != null)
            {
                foreach (int m in this.MetaData)
                {
                    int i = 1;
                    foreach (Node c in this.Children)
                    {
                        if (m == i)
                        {
                            if (c.Children == null)
                            {
                                this.ChildSum += c.MetaDataSum;
                            }
                            else
                            {
                                this.ChildSum += c.ChildSum;
                            }
                        }
                        i++;
                    }
                }
            }
        }
    }
}
