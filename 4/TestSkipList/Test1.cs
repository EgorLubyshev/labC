 
[TestClass]
public class SkipListTests
{
    [TestMethod]
    public void Insert_Search_ShouldFindInsertedItem()
    {
        var skipList = new SkipList<int, string>();
        skipList.Insert(1, "one");
        skipList.Insert(2, "two");

        Assert.IsTrue(skipList.Search(1, out var val1));
        Assert.AreEqual("one", val1);

        Assert.IsTrue(skipList.Search(2, out var val2));
        Assert.AreEqual("two", val2);
    }

    [TestMethod]
    public void Delete_ShouldRemoveItem()
    {
        var skipList = new SkipList<int, string>();
        skipList.Insert(1, "one");
        skipList.Insert(2, "two");

        Assert.IsTrue(skipList.Delete(1));
        Assert.IsFalse(skipList.Search(1, out _));
    }

    [TestMethod]
    public void Delete_NonExisting_ShouldReturnFalse()
    {
        var skipList = new SkipList<int, string>();
        skipList.Insert(1, "one");

        Assert.IsFalse(skipList.Delete(2));
    }
}
