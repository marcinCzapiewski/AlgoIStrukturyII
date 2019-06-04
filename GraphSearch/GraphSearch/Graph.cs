using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GraphSearch
{
    class Graph
    {
        public readonly int MaxVertices = 30;

        public int Vertices { get; set; }
        public int Edges { get; set; }

        bool[][] adjencyList;
        Vertex[] vertexList;

        public Graph()
        {
            adjencyList = new bool[MaxVertices][];
            for (int i = 0; i < adjencyList.Length; i++)
            {
                adjencyList[i] = new bool[MaxVertices];
            }

            vertexList = new Vertex[MaxVertices];
        }

        public void DFS(int value)
        {
            var visited = new bool[Vertices];

            DFSUtil(value, visited);
        }

        private void DFSUtil(int v, bool[] visited)
        {
            visited[v] = true;
            Console.Write(v + " ");

            for (int i = 0; i < visited.Length; i++)
            {
                if (!visited[i])
                {
                    DFSUtil(i, visited);
                }
            }
        }


        public void InsertVertex(int value)
        {
            vertexList[Vertices++] = new Vertex(value);
        }

        public void InsertEdge(int val1, int val2)
        {
            int u = GetIndex(val1);
            int v = GetIndex(val2);

            if (u == v)
                throw new InvalidOperationException("Not a valid edge");

            if (adjencyList[u][v])
                Console.WriteLine("Edge already exists");
            else
            {
                adjencyList[u][v] = true;
                Edges++;
            }
        }

        private int GetIndex(int val)
        {
            for (int i = 0; i < Vertices; i++)
            {
                if (val.Equals(vertexList[i].Value))
                    return i;
            }

            throw new InvalidOperationException("Invalid vertex");
        }
    }
}
