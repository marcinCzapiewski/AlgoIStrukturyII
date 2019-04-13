namespace BTree
{
    using System;
    using System.Collections.Generic;

    public class Node<TK>
    {
        private readonly int _degree;

        public Node(int degree)
        {
            _degree = degree;
            Children = new List<Node<TK>>(degree);
            Entries = new List<Entry<TK>>(degree);
        }

        public List<Node<TK>> Children { get; set; }

        public List<Entry<TK>> Entries { get; set; }

        public bool IsLeaf
        {
            get
            {
                return Children.Count == 0;
            }
        }

        public bool HasReachedMaxEntries
        {
            get
            {
                return Entries.Count == (2 * _degree) - 1;
            }
        }

        public bool HasReachedMinEntries
        {
            get
            {
                return Entries.Count == _degree - 1;
            }
        }

        public void Traverse()
        {
            int i;
            for (i = 0; i < Entries.Count; i++)
            {
                if (!IsLeaf)
                {
                    Children[i].Traverse();
                }

                Console.Write(" " + Entries[i].Key);
            }

            if (!IsLeaf)
            {
                Children[i].Traverse();
            }
        }
    }
}
