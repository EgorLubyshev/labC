using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string text = File.ReadAllText("WarAndWorld.txt");
        var words = text.Split(new[] { ' ', '\n', '\r', ',', '.', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

        MeasurePerformance(new Dictionary<string, int>(), words, "Dictionary");
        MeasurePerformance(new HashTable<string, int>(), words, "HashTable");
    }

    static void MeasurePerformance(IDictionary<string, int> dict, string[] words, string name)
    {
        var stopwatch = Stopwatch.StartNew();

        foreach (var word in words)
        {
            var w = word.ToLowerInvariant();
            if (dict.ContainsKey(w))
                dict[w]++;
            else
                dict[w] = 1;
        }

        var selected = dict.Where(p => p.Value > 27).Select(p => p.Key).ToList();

        foreach (var word in selected)
            dict.Remove(word);

        stopwatch.Stop();
        Console.WriteLine($"{name} completed in {stopwatch.ElapsedMilliseconds} ms");
    }
}
