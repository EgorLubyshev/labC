using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace Stack
{
   
    internal class Program
    {
        static void Main(string[] args)
        {
            const int n = 100000;

            var myStack = new MyStack<int>();
            var sw1 = Stopwatch.StartNew();
            for (int i = 0; i < n; i++)
                myStack.Push(i);
            for (int i = 0; i < n / 2; i++)
                myStack.Pop();
            for (int i = n / 2; i < n; i++)
                myStack.Contains(i);
            sw1.Stop();
            Console.WriteLine($"MyStack<T> time: {sw1.ElapsedMilliseconds} ms");
             
            var systemStack = new Stack<int>();
            var sw2 = Stopwatch.StartNew();
            for (int i = 0; i < n; i++)
                systemStack.Push(i);
            for (int i = 0; i < n / 2; i++)
                systemStack.Pop();
            for (int i = n / 2; i < n; i++)
                systemStack.Contains(i);
            sw2.Stop();
            Console.WriteLine($"System.Stack<T> time: {sw2.ElapsedMilliseconds} ms");
        }
    }
}
