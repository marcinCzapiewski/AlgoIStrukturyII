using System;
using System.Collections.Generic;
using System.Text;

namespace DisjointSets
{
    class Node
    {
        public int Key { get; set; }
        public Node Parent { get; set; }
        public int Rank { get; set; }

        public Node(int key, int rank, Node parent)
        {
            Key = key;
            Parent = parent??this;
            Rank = rank;
        }
    }
}
