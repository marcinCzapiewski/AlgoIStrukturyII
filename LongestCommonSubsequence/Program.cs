using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestCommonSubsequence
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new LongestCommonSubsequenceGenerator("agtgatg", "gttag");
            System.Console.WriteLine("LCS length: " + generator.GetLcsLength());

            var longestCommonSubsequences = generator.GetAllLongestCommonSubSequences();

            foreach (var subsequence in longestCommonSubsequences)
            {
                System.Console.WriteLine(subsequence);
            }
        }
    }

    class LongestCommonSubsequenceGenerator
    {
        private readonly string _s1;
        private readonly string _s2;
        private readonly int[,] array;

        public LongestCommonSubsequenceGenerator(string s1, string s2)
        {
            _s1 = s1;
            _s2 = s2;
            var arraySize = Math.Max(s1.Length + 1, s2.Length + 1);
            array = new int[100, 100];
        }

        public HashSet<string> GetAllLongestCommonSubSequences()
        {
            var allCommon = FindAllCommonSubSequences(_s1.Length, _s2.Length);
            var maxLength = allCommon.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur).Length;

            return allCommon.Where(x => x.Length == maxLength).ToHashSet();
        }

        private HashSet<string> FindAllCommonSubSequences(int i, int j)
        {
            var longestCommonSubSequences = new HashSet<string>();

            if (i == 0 || j == 0)
            {
                longestCommonSubSequences.Add("");
                return longestCommonSubSequences;
            }

            if (_s1[i - 1] == _s2[j - 1])
            {
                var subsequences = FindAllCommonSubSequences(i - 1, j - 1);

                foreach(var subSequence in subsequences)
                {
                    longestCommonSubSequences.Add(subSequence + _s1[i - 1]);
                }
            }
            else
            {
                if (array[i - 1, j] >= array[j, i - 1])
                {
                    longestCommonSubSequences = FindAllCommonSubSequences(i - 1, j);
                }

                if (array[i, j - 1] >= array[i - 1, j])
                {
                    var tmp = FindAllCommonSubSequences(i, j - 1);

                    longestCommonSubSequences.UnionWith(tmp);
                }
            }

            return longestCommonSubSequences;
        }

        public int GetLcsLength()
        {
            var i = _s1.Length;
            var j = _s2.Length;
            
            for (int k = 0; k <= i; k++)
            {
                for (int l = 0; l <= j; l++)
                {
                    if (k == 0 || l == 0)
                    {
                        array[k, l] = 0;
                    }
                    else if (_s1[k - 1] == _s2[l - 1])
                    {
                        array[k, l] = array[k - 1, l - 1] + 1;
                    }
                    else
                    {
                        array[k, l] = Math.Max(array[k - 1, l], array[k, l - 1]);
                    }
                }
            }

            return array[i, j];
        }

    }
}
