using System.Diagnostics;
using ClassHeap;
class Program
{
    static void Main()
    {
        var scenarios = new Dictionary<string, int[]>
        {
            ["Sorted"] = Enumerable.Range(0, 10000).ToArray(),
            ["Partially Sorted"] = Enumerable.Range(0, 10000).OrderBy(x => x % 100).ToArray(),
            ["Random Distinct"] = Enumerable.Range(0, 10000).OrderBy(_ => Guid.NewGuid()).ToArray(),
            ["Random With Duplicates"] = Enumerable.Range(0, 10000).Select(_ => Random.Shared.Next(0, 100)).ToArray(),
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
