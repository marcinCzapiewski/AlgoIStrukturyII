using System.Collections.Generic;
using System.Linq;

namespace DisjointSets
{
    static class DisjointSets
    {
        static public Node MakeSet(int key)
        {
            var node = new Node(key, 0, null);

            return node;
        }

        public static Node FindSet(Node x)
        {
            if (x != x.Parent)
            {
                var tmp = FindSet(x.Parent);
                x.Parent = tmp;
            }

            return x.Parent;
        }

        public static void Union(Node x, Node y)
        {
            if (x.Rank > y.Rank)
            {
                y.Parent = x;
            }
            else
            {
                x.Parent = y;
                if (x.Rank == y.Rank)
                {
                    y.Rank++;
                }
            }
        }
    }
}
