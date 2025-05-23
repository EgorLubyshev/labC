using System.Text.RegularExpressions;
using System.Text;
using Strings;
namespace TestStrings
{
    [TestClass]
    public class SubstringSearchTests
    {
        private readonly List<ISubstringSearch> algms = new()
    {
        new BruteForceAlgorithm(),
        new RabinKarpAlgorithm(),
        new KMPAlgorithm(),
        new BoyerMooreAlgorithm()
    };

        [TestMethod]
        public void SimpleRepeatedPatternTest()
        {
            string text = "aaaaaaaaaa";
            string pattern = "aa";
            var expected = Enumerable.Range(0, 9).ToList();

            foreach (var algm in algms)
            {
                var actual = algm.IndexesOf(pattern, text);
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void SearchBagOfWordsOnAnnaTxt()
        {
            string text;
            using (var sr = new StreamReader("anna.txt", Encoding.UTF8))
                text = sr.ReadToEnd().ToLower();

            var bag = Regex.Matches(text, @"\w+")
                           .Cast<Match>()
                           .Select(m => m.Value)
                           .Distinct()
                           .Take(100)
                           .ToList();

            var brute = new BruteForceAlgorithm();
            foreach (var pattern in bag)
            {
                var expected = brute.IndexesOf(pattern, text);
                foreach (var algm in algms.Where(a => a.GetType() != typeof(BruteForceAlgorithm)))
                {
                    var actual = algm.IndexesOf(pattern, text);
                    CollectionAssert.AreEqual(expected, actual, $"Failed on pattern '{pattern}' in {algm.GetType().Name}");
                }
            }
        }
    }

}
