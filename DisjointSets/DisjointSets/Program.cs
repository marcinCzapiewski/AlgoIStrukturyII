using System;

namespace DisjointSets
{
    class Program
    {
        static void Main(string[] args)
        {
            int vertices = 6;
            var graph = new Graph(vertices);
            graph.AddEgde(0, 1);
            graph.AddEgde(0, 2);
            graph.AddEgde(1, 3);
            graph.AddEgde(4, 5);
            Console.WriteLine("Disjoint Sets: ");
            graph.DisjointSets();
        }
    }
}
