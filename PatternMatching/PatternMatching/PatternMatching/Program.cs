using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PatternMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("NAIWNY");
            Console.WriteLine();
            Console.WriteLine("Wzorzec 1");
            Console.WriteLine("----------------------------------------");

            string text = File.ReadAllText(@"tekst.txt", Encoding.UTF8);
            string pattern = File.ReadAllText(@"wzorzec.txt", Encoding.UTF8);

            var naivePattern1Watch = Stopwatch.StartNew();
            PatternMatcher.NaiveSearch(text, pattern);
            naivePattern1Watch.Stop();

            Console.WriteLine("Wzorzec 2");
            Console.WriteLine("----------------------------------------");

            text = File.ReadAllText(@"tekst1.txt", Encoding.UTF8);
            pattern = File.ReadAllText(@"wzorzec1.txt", Encoding.UTF8);

            var naivePattern2Watch = Stopwatch.StartNew();
            PatternMatcher.NaiveSearch(text, pattern);
            naivePattern2Watch.Stop();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("Rabin-Karp");
            Console.WriteLine();
            Console.WriteLine("Wzorzec 1");
            Console.WriteLine("----------------------------------------");

            text = File.ReadAllText(@"tekst.txt", Encoding.UTF8);
            pattern = File.ReadAllText(@"wzorzec.txt", Encoding.UTF8);

            var rabinKarpPattern1Watch = Stopwatch.StartNew();
            PatternMatcher.RabinKarpSearch(text, pattern, 11);
            rabinKarpPattern1Watch.Stop();

            Console.WriteLine("Wzorzec 2");
            Console.WriteLine("----------------------------------------");

            text = File.ReadAllText(@"tekst1.txt", Encoding.UTF8);
            pattern = File.ReadAllText(@"wzorzec1.txt", Encoding.UTF8);

            var rabinKarpPattern2Watch = Stopwatch.StartNew();
            PatternMatcher.RabinKarpSearch(text, pattern, 11);
            rabinKarpPattern2Watch.Stop();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("KMP");
            Console.WriteLine();
            Console.WriteLine("Wzorzec 1");
            Console.WriteLine("----------------------------------------");

            text = File.ReadAllText(@"tekst.txt", Encoding.UTF8);
            pattern = File.ReadAllText(@"wzorzec.txt", Encoding.UTF8);

            var kmpPattern1Watch = Stopwatch.StartNew();
            PatternMatcher.KMPSearch(text, pattern);
            kmpPattern1Watch.Stop();

            Console.WriteLine("Wzorzec 2");
            Console.WriteLine("----------------------------------------");

            text = File.ReadAllText(@"tekst1.txt", Encoding.UTF8);
            pattern = File.ReadAllText(@"wzorzec1.txt", Encoding.UTF8);

            var kmpPattern2Watch = Stopwatch.StartNew();
            PatternMatcher.KMPSearch(text, pattern);
            kmpPattern2Watch.Stop();


            Console.WriteLine("WZORZEC 1: ");
            Console.WriteLine("Naiwny: " + naivePattern1Watch.ElapsedMilliseconds);
            Console.WriteLine("Rabin-Karp: " + rabinKarpPattern1Watch.ElapsedMilliseconds);
            Console.WriteLine("KMP: " + kmpPattern1Watch.ElapsedMilliseconds);

            Console.WriteLine("WZORZEC 2: ");
            Console.WriteLine("Naiwny: " + naivePattern2Watch.ElapsedMilliseconds);
            Console.WriteLine("Rabin-Karp: " + rabinKarpPattern2Watch.ElapsedMilliseconds);
            Console.WriteLine("KMP: " + kmpPattern2Watch.ElapsedMilliseconds);
        }
    }
}
