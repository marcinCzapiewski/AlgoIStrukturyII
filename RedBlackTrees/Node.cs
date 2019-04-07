    namespace RedBlackTree 
    {
        public class Node
        {
            public Colour Colour { get; set; }
            public Node LeftChild { get; set; }
            public Node RightChild { get; set; }
            public Node Parent { get; set; }
            public int Value { get; set; }
 
            public Node(int value) { this.Value = value; }
        }
    }