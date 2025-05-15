using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

[TestClass]
public class HashTableTests
{
    [TestMethod]
    public void AddAndRetrieveTest()
    {
        var table = new HashTable<string, int>();
        table.Add("apple", 1);
        Assert.AreEqual(1, table["apple"]);
    }

    [TestMethod]
    public void RemoveTest()
    {
        var table = new HashTable<string, int>();
        table.Add("apple", 1);
        table.Remove("apple");
        Assert.IsFalse(table.ContainsKey("apple"));
    }

    [TestMethod]
    public void TryGetValueTest()
    {
        var table = new HashTable<string, int>();
        table.Add("apple", 1);
        Assert.IsTrue(table.TryGetValue("apple", out var value));
        Assert.AreEqual(1, value);
    }

    [TestMethod]
    public void ResizeTest()
    {
        var table = new HashTable<int, int>();
        for (int i = 0; i < 100; i++)
            table.Add(i, i * 10);

        Assert.AreEqual(100, table.Count);
        for (int i = 0; i < 100; i++)
            Assert.AreEqual(i * 10, table[i]);
    }
}
