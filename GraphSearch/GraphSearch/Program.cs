using System;

namespace GraphSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var graph = new Graph();

            graph.InsertVertex(0);
            graph.InsertVertex(1);
            graph.InsertVertex(2);
            graph.InsertVertex(3);

            graph.InsertEdge(0, 1);
            graph.InsertEdge(0, 2);
            graph.InsertEdge(1, 2);
            graph.InsertEdge(2, 0);
            graph.InsertEdge(2, 3);

            graph.DFS(2);
            Console.WriteLine();
        }
    }
}
