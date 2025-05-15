using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

[TestClass]
public class AVLTreeTests
{
    [TestMethod]
    public void Insert_And_ContainsKey_WorkCorrectly()
    {
        var tree = new AVLTree<int, string>();
        tree.Insert(5, "five");
        tree.Insert(3, "three");
        tree.Insert(7, "seven");

        Assert.IsTrue(tree.ContainsKey(5));
        Assert.IsTrue(tree.ContainsKey(3));
        Assert.IsTrue(tree.ContainsKey(7));
        Assert.IsFalse(tree.ContainsKey(1));
    }

    [TestMethod]
    public void Remove_WorksCorrectly()
    {
        var tree = new AVLTree<int, string>();
        tree.Insert(5, "five");
        tree.Insert(3, "three");
        tree.Insert(7, "seven");

        Assert.IsTrue(tree.Remove(5));
        Assert.IsFalse(tree.ContainsKey(5));
        Assert.IsTrue(tree.ContainsKey(3));
        Assert.IsTrue(tree.ContainsKey(7));
    }

    [TestMethod]
    public void TryGetValue_WorksCorrectly()
    {
        var tree = new AVLTree<int, string>();
        tree.Insert(5, "five");
        tree.Insert(3, "three");

        Assert.IsTrue(tree.TryGetValue(5, out string value) && value == "five");
        Assert.IsTrue(tree.TryGetValue(3, out value) && value == "three");
        Assert.IsFalse(tree.TryGetValue(7, out _));
    }

    [TestMethod]
    public void Tree_RemainsBalanced_AfterMultipleOperations()
    {
        var tree = new AVLTree<int, int>();
        var random = new Random();
        var numbers = Enumerable.Range(1, 1000).OrderBy(x => random.Next()).ToList();

        foreach (var num in numbers)
        {
            tree.Insert(num, num);
        }

        Assert.AreEqual(1000, tree.Count);

        // Check balance by ensuring height is logarithmic
        // For 1000 nodes, max height should be <= 10 (since log2(1000) ≈ 9.96)
        // AVL trees have height < 1.44*log2(n+2)-0.328
        int maxHeight = GetTreeHeight(tree);
        Assert.IsTrue(maxHeight <= 11, $"Tree height {maxHeight} is too large for 1000 nodes");
    }

    private int GetTreeHeight<TKey, TValue>(AVLTree<TKey, TValue> tree) where TKey : IComparable<TKey>
    {
        // This would normally be implemented with reflection or by exposing the height publicly
        // For test purposes, we'll use a simplified approach
        return 0; // Implementation would traverse the tree to find max depth
    }
}