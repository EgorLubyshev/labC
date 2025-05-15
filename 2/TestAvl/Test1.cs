using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AVLTreeTests
{
    [TestMethod]
    public void TestInsertAndSearch()
    {
        var tree = new AVLTree<int, string>();
        tree.Insert(10, "A");
        tree.Insert(20, "B");
        tree.Insert(5, "C");

        Assert.IsTrue(tree.Search(10));
        Assert.IsTrue(tree.Search(20));
        Assert.IsTrue(tree.Search(5));
        Assert.IsFalse(tree.Search(15));
    }

    [TestMethod]
    public void TestDelete()
    {
        var tree = new AVLTree<int, string>();
        tree.Insert(10, "A");
        tree.Insert(20, "B");
        tree.Insert(5, "C");

        tree.Delete(10);
        Assert.IsFalse(tree.Search(10));
        Assert.IsTrue(tree.Search(5));
        Assert.IsTrue(tree.Search(20));
    }
}
