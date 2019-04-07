using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;

namespace HuffmanCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Opening a file...");

            var fileToEncodePath = @"C:\Users\Marcin\Downloads\tekst-dlugi.txt";
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileToEncodePath);

            string text;
            var fileStream = new FileStream(fileToEncodePath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }

            Console.WriteLine($"Length in bits before encoding: {new BitArray(text.Select(c => c == '1').ToArray()).Length * 8}");

            string input = text;
            var huffmanTree = new HuffmanTree();

            // Build the Huffman tree
            Console.WriteLine("Building the Huffman tree...");
            huffmanTree.Build(input);

            // Encode
            Console.WriteLine("Encoding...");
            BitArray encoded = huffmanTree.Encode(input);


            foreach (var pair in huffmanTree.Frequencies)
            {
                Console.WriteLine($"Char: {pair.Key}, Frequency: {pair.Value}, Encoding: {new string(huffmanTree.Encodings[pair.Key].Select(x => x ? '1' : '0').ToArray())}");
            }

            byte[] bytes = new byte[encoded.Length / 8 + (encoded.Length % 8 == 0 ? 0 : 1)];
            encoded.CopyTo(bytes, 0);
            Console.WriteLine($"Length in bits after encoding: {bytes.Length * 8}");


            File.WriteAllBytes($@"C:\Users\Marcin\Downloads\{fileNameWithoutExtension}.huff", bytes);

            Console.WriteLine();
            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}
