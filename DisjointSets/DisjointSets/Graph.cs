using System;
using System.Collections.Generic;

namespace DisjointSets
{
    class Graph
    {
        private readonly int _vertices;
        private LinkedList<Edge>[] _adjList;
        private readonly List<Edge> _allEdges = new List<Edge>();

        public Graph(int vertices)
        {
            _vertices = vertices;
            _adjList = new LinkedList<Edge>[vertices];
            for (int i = 0; i < vertices; i++)
            {
                _adjList[i] = new LinkedList<Edge>();
            }
        }

        public void AddEgde(int source, int destination)
        {
            var edge = new Edge(source, destination);
            _adjList[source].AddFirst(edge);
            _allEdges.Add(edge);
        }

        public int Find(Subset[] subSet, int vertex)
        {
            if (subSet[vertex].Parent != vertex)
                return Find(subSet, subSet[vertex].Parent);

            return vertex;
        }

        public void Union(Subset[] subSet, int x, int y)
        {
            int x_set_parent = Find(subSet, x);
            int y_set_parent = Find(subSet, y);

            if (subSet[x_set_parent].Rank > subSet[y_set_parent].Rank)
            {
                subSet[y_set_parent].Parent = x_set_parent;
            }

            else if (subSet[y_set_parent].Rank < subSet[y_set_parent].Rank)
            {
                subSet[x_set_parent].Parent = y_set_parent;
            }

            else
            {
                subSet[y_set_parent].Parent = x_set_parent;
                subSet[x_set_parent].Rank++;
            }
        }

        public void MakeSet(Subset[] subSets)
        {
            for (int i = 0; i < _vertices; i++)
            {
                subSets[i] = new Subset
                {
                    Parent = i,
                    Rank = 0
                };
            }
        }

        public void DisjointSets()
        {
            var subsets = new Subset[_vertices];

            MakeSet(subsets);

            for (int i = 0; i < _allEdges.Count; i++)
            {
                Edge edge = _allEdges[i];
                int x_set = Find(subsets, edge.Source);
                int y_set = Find(subsets, edge.Destination);

                if (x_set != y_set)
                {
                    Union(subsets, x_set, y_set);
                }                    
            }

            PrintSets(subsets);
        }

        public void PrintSets(Subset[] subSets)
        {
            var map = new Dictionary<int, List<int>>();
            for (int i = 0; i < subSets.Length; i++)
            {
                if (map.ContainsKey(subSets[i].Parent))
                {
                    var list = map[subSets[i].Parent];
                    list.Add(i);
                    map.TryAdd(subSets[i].Parent, list);
                }
                else
                {
                    var list = new List<int>();
                    list.Add(i);
                    map.Add(subSets[i].Parent, list);
                }
            }

            var set = map.Keys;

            var iterator = set.GetEnumerator();
            while (iterator.MoveNext())
            {
                int key = iterator.Current;
                Console.WriteLine("Set Id: " + key + " elements: ");
                map[key].ForEach(x => Console.Write($"{x} "));
                Console.WriteLine();
            }
        }

        public class Edge
        {
            public int Source { get; set; }
            public int Destination { get; set; }

            public Edge(int source, int destination)
            {
                Source = source;
                Destination = destination;
            }
        }

        public class Subset
        {
            public int Parent { get; set; }
            public int Rank { get; set; }
        }
    }
}
