using BTree;
using System;

namespace BTreesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var bTree = new BTree<int>(3);

            bTree.Insert(1);
            bTree.Insert(2);
            bTree.Insert(3);
            bTree.Insert(4);
            bTree.Insert(5);
            bTree.Insert(6);
            bTree.Insert(7);
            bTree.Insert(8);
            bTree.Insert(9);
            bTree.Insert(10);
            bTree.Insert(11);
            bTree.Insert(12);
            bTree.Insert(13);
            bTree.Insert(14);
            bTree.Insert(15);
            bTree.Insert(16);
            bTree.Insert(17);

            bTree.Traverse();
            Console.WriteLine();

            bTree.Print();


        }
    }
}
