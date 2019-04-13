using BTree;
using System;

namespace BTreesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var bTree = new BTree<int, object>(3);

            bTree.Insert(1, null);
            bTree.Insert(2, null);
            bTree.Insert(3, null);
            bTree.Insert(4, null);
            bTree.Insert(5, null);
            bTree.Insert(6, null);
            bTree.Insert(7, null);
            bTree.Insert(8, null);
            bTree.Insert(9, null);
            bTree.Insert(10, null);
            bTree.Insert(11, null);
            bTree.Insert(12, null);
            bTree.Insert(13, null);
            bTree.Insert(14, null);
            bTree.Insert(15, null);
            bTree.Insert(16, null);
            bTree.Insert(17, null);

            bTree.Traverse();
            Console.WriteLine();

            bTree.Print();


        }
    }
}
