using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;

namespace UnitTestProject1
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Push_IncreasesCount()
        {
            var stack = new MyStack<int>();
            stack.Push(10);
            Assert.AreEqual(1, stack.Count);
        }

        [TestMethod]
        public void Pop_ReturnsLastItemAndDecreasesCount()
        {
            var stack = new MyStack<string>();
            stack.Push("hello");
            var item = stack.Pop();
            Assert.AreEqual("hello", item);
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void Peek_ReturnsLastItemWithoutRemoving()
        {
            var stack = new MyStack<int>();
            stack.Push(5);
            var top = stack.Peek();
            Assert.AreEqual(5, top);
            Assert.AreEqual(1, stack.Count);
        }

        [TestMethod]
        public void Contains_ReturnsTrueIfItemExists()
        {
            var stack = new MyStack<string>();
            stack.Push("apple");
            stack.Push("banana");
            Assert.IsTrue(stack.Contains("banana"));
            Assert.IsFalse(stack.Contains("cherry"));
        }

        [TestMethod]
        public void Pop_EmptyStack_ThrowsException()
        {
            var stack = new MyStack<int>();
            Assert.ThrowsException<InvalidOperationException>(() => stack.Pop());
        }
    }
}
