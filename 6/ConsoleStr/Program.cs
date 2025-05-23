using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Strings;
class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Write("Введите подстроку для поиска: ");
        string pattern = Console.ReadLine();

        string text = File.ReadAllText("alphabet.txt", Encoding.UTF8);

        var algorithms = new List<(string Name, ISubstringSearch Alg)>
        {
            ("Brute Force", new BruteForceAlgorithm()),
            ("Rabin-Karp", new RabinKarpAlgorithm()),
            ("KMP", new KMPAlgorithm()),
            ("Boyer-Moore", new BoyerMooreAlgorithm())
        };
        
        foreach (var (name, alg) in algorithms)
        {
            var sw = Stopwatch.StartNew();
            var indexes = alg.IndexesOf(pattern, text);
            sw.Stop();
            Console.WriteLine($"{name}: найдено {indexes.Count} вхождений, время {sw.ElapsedMilliseconds} мс");
        }
    }
}
