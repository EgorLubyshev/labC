using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        int n = 100000;
        var rnd = new Random();
        var array = new int[n];
        for (int i = 0; i < n; i++)
            array[i] = rnd.Next();

        // AVLTree
        var avl = new AVLTree<int, int>();
        foreach (var num in array)
            avl.Insert(num, num);

        for (int i = n / 2; i < 3 * n / 4; i++)
            avl.Delete(array[i]);

        var sw = Stopwatch.StartNew();
        foreach (var num in array)
            avl.Search(num);
        sw.Stop();
        Console.WriteLine($"AVLTree Search Time: {sw.ElapsedMilliseconds} ms");

        // SortedDictionary
        var dict = new SortedDictionary<int, int>();
        foreach (var num in array)
            dict[num] = num;

        for (int i = n / 2; i < 3 * n / 4; i++)
            dict.Remove(array[i]);

        sw.Restart();
        foreach (var num in array)
            dict.ContainsKey(num);
        sw.Stop();
        Console.WriteLine($"SortedDictionary Search Time: {sw.ElapsedMilliseconds} ms");
    }
}
