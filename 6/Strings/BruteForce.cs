using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strings
{
    public class BruteForceAlgorithm : ISubstringSearch
    {
        //N*M
        public List<int> IndexesOf(string pattern, string text)
        {
            var result = new List<int>();
            for (int i = 0; i <= text.Length - pattern.Length; i++)
            {
                int j = 0;
                while (j < pattern.Length && text[i + j] == pattern[j])
                    j++;
                if (j == pattern.Length)
                    result.Add(i);
            }
            return result;
        }
    }
}
