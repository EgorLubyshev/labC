using ClassHeap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BinaryHeapTests
{
    [TestClass]
    public class BinaryHeapTests
    {
        [TestMethod]
        public void Insert_And_Extract_MinHeap_Should_Work()
        {
            var heap = new BinaryHeap<int>();
            heap.Insert(3);
            heap.Insert(1);
            heap.Insert(2);

            Assert.AreEqual(1, heap.Extract());
            Assert.AreEqual(2, heap.Extract());
            Assert.AreEqual(3, heap.Extract());
        }

        [TestMethod]
        public void Insert_And_Extract_MaxHeap_Should_Work()
        {
            var heap = new BinaryHeap<int>(Comparer<int>.Create((a, b) => b - a));
            heap.Insert(1);
            heap.Insert(3);
            heap.Insert(2);

            Assert.AreEqual(3, heap.Extract());
            Assert.AreEqual(2, heap.Extract());
            Assert.AreEqual(1, heap.Extract());
        }

        [TestMethod]
        public void Peek_Should_Return_Min_Or_Max()
        {
            var heap = new BinaryHeap<int>();
            heap.Insert(10);
            heap.Insert(5);

            Assert.AreEqual(5, heap.Peek());
            Assert.AreEqual(2, heap.Count);  
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Extract_On_Empty_Heap_Should_Throw()
        {
            var heap = new BinaryHeap<int>();
            heap.Extract();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Peek_On_Empty_Heap_Should_Throw()
        {
            var heap = new BinaryHeap<int>();
            heap.Peek();
        }
    }
}
