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
                x.Parent = FindSet(x.Parent);
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
                    y.Rank += 1;
                }
            }
        }
    }
}
