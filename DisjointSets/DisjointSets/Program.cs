using System;
using System.Collections.Generic;
using System.Linq;
using static DisjointSets.DisjointSets;

namespace DisjointSets
{
    class Program
    {
        static void Main(string[] args)
        {
            var sets = new List<Node>();
            for (int i = 1; i < 10; i++)
            {
                sets.Add(MakeSet(i));
            }

            Union(FindSet(sets[0]), FindSet(sets[1]));
            Union(FindSet(sets[2]), FindSet(sets[3]));
            Union(FindSet(sets[4]), FindSet(sets[3]));
            Union(FindSet(sets[0]), FindSet(sets[4]));
            Union(FindSet(sets[5]), FindSet(sets[6]));
            Union(FindSet(sets[7]), FindSet(sets[8]));
            Union(FindSet(sets[5]), FindSet(sets[7]));
            Union(FindSet(sets[6]), FindSet(sets[3]));

            foreach(var node in sets.OrderByDescending(x => x.Rank))
            {
                Console.Write($"{node.Key} ");
            }
        }
    }
}
