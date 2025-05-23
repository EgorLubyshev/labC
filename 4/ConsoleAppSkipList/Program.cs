using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        const int n = 100000;
        var rand = new Random();
        int[] array = new int[n];
        for (int i = 0; i < n; i++)
        {
            array[i] = rand.Next(0, 100000);
        }

        var skipList = new SkipList<int, int>();
        var sortedList = new SortedList<int, int>();

        foreach (var item in array)
        {
            skipList.Insert(item, item);
            if (!sortedList.ContainsKey(item))
                sortedList[item] = item;
        }

        
        for (int i = n / 2; i < 3 * n / 4; i++)
        {
            skipList.Delete(array[i]);
            sortedList.Remove(array[i]);
        }

        Stopwatch sw = new Stopwatch();

        sw.Start();
        foreach (var item in array)
        {
            skipList.Search(item, out _);
        }
        sw.Stop();
        Console.WriteLine($"SkipList search time: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        foreach (var item in array)
        {
            sortedList.TryGetValue(item, out _);
        }
        sw.Stop();
        Console.WriteLine($"SortedList search time: {sw.ElapsedMilliseconds} ms");
    }
}
