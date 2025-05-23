using System.Diagnostics;
using ClassHeap;
class Program
{
    static void Main()
    {
        int range = 10000;
        var scenarios = new Dictionary<string, int[]>
        {
            ["Sorted"] = Enumerable.Range(0, range).ToArray(),
            ["Partially Sorted"] = Enumerable.Range(0, range).OrderBy(x => x % 100).ToArray(),
            ["Random Distinct"] = Enumerable.Range(0, range).OrderBy(_ => Guid.NewGuid()).ToArray(),
            ["Random With Duplicates"] = Enumerable.Range(0, range).Select(_ => Random.Shared.Next(0, 1000)).ToArray(),
        };

        foreach (var (name, original) in scenarios)
        {
            Console.WriteLine($"\n{name}:");

            var quick = (int[])original.Clone();
            var heap = (int[])original.Clone();

            var sw = Stopwatch.StartNew();
            SortingAlgorithms.QuickSort(quick);
            sw.Stop();
            Console.WriteLine($"QuickSort: {sw.ElapsedMilliseconds} ms");

            sw.Restart();
            SortingAlgorithms.HeapSort(heap);
            sw.Stop();
            Console.WriteLine($"HeapSort: {sw.ElapsedMilliseconds} ms");
        }
    }
}
