using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PatternMatching
{
    public static class PatternMatcher
    {
        public static void NaiveSearch(string text, string pattern)
        {
            text = Regex.Replace(text, @"\s+", string.Empty);
            pattern = Regex.Replace(pattern, @"\s+", string.Empty);

            var textLength = text.Length;
            var patternLength = pattern.Length;

            int j;
            for (int i = 0; i <= textLength - patternLength; i++)
            {
                for (j = 0; j < pattern.Length; j++)
                {
                    if (char.IsWhiteSpace(text[i + j]) || char.IsWhiteSpace(pattern[j]))
                    {
                        continue;
                    }
                    if (text[i + j] != pattern[j])
                    {
                        break;
                    }
                }

                if (j == patternLength)
                {
                    Console.WriteLine("Pattern found at index: " + i);
                }
            }
        }

        public static void RabinKarpSearch(string text, string pattern, int primeNumber)
        {
            text = Regex.Replace(text, @"\s+", string.Empty);
            pattern = Regex.Replace(pattern, @"\s+", string.Empty);

            var characterCount = text.Distinct().Count();
            var patternLength = pattern.Length;
            var textLength = text.Length;
            int i, j;

            int p = 0;
            int t = 0;
            int h = 1;

            for (i = 0; i < patternLength - 1; i++)
            {
                h = (h * characterCount) % primeNumber;
            }

            for (i = 0; i < patternLength; i++)
            {
                p = (characterCount * p + pattern[i]) % primeNumber;
                t = (characterCount * t + text[i]) % primeNumber;
            }

            for (i = 0; i <= textLength - patternLength; i++)
            {
                if (p == t)
                {
                    for (j = 0; j < patternLength; j++)
                    {
                        if (text[i + j] != pattern[j])
                            break;
                    }

                    if (j == patternLength)
                        Console.WriteLine("Pattern found at index " + i);
                }

                if (i < textLength - patternLength)
                {
                    t = (characterCount * (t - text[i] * h) + text[i + patternLength]) % primeNumber;

                    if (t < 0)
                        t = (t + primeNumber);
                }
            }
        }
        public static void KMPSearch(string text, string pattern)
        {
            text = Regex.Replace(text, @"\s+", string.Empty);
            pattern = Regex.Replace(pattern, @"\s+", string.Empty);

            var patternLength = pattern.Length;
            var textLength = text.Length;

            var lps = new int[patternLength];
            var j = 0;

            ComputeLPSArray(pattern, patternLength, lps);

            int i = 0;
            while (i < textLength)
            {
                if (pattern[j] == text[i])
                {
                    j++;
                    i++;
                }
                if (j == patternLength)
                {
                    Console.WriteLine("Found pattern "
                                  + "at index " + (i - j));
                    j = lps[j - 1];
                }

                else if (i < textLength && pattern[j] != text[i])
                {
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i += 1;
                }
            }
        }

        private static void ComputeLPSArray(string pattern, int M, int[] lps)
        {
            int len = 0;
            int i = 1;
            lps[0] = 0;

            while (i < M)
            {
                if (pattern[i] == pattern[len])
                {
                    
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i] = len;
                        i++;
                    }
                }
            }
        }
    }
}
