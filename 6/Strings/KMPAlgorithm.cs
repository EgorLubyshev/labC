using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strings
{
    public class KMPAlgorithm : ISubstringSearch
    {
        public List<int> IndexesOf(string pattern, string text)
        {
            var result = new List<int>();
            int[] lps = BuildLpsArray(pattern);
            int i = 0, j = 0;
            while (i < text.Length)
            {
                if (pattern[j] == text[i])
                {
                    i++; j++;
                }

                if (j == pattern.Length)
                {
                    result.Add(i - j);
                    j = lps[j - 1];
                }
                else if (i < text.Length && pattern[j] != text[i])
                {
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i++;
                }
            }
            return result;
        }

        private int[] BuildLpsArray(string pattern)
        {
            int[] lps = new int[pattern.Length];
            int length = 0;
            for (int i = 1; i < pattern.Length;)
            {
                if (pattern[i] == pattern[length])
                    lps[i++] = ++length;
                else if (length != 0)
                    length = lps[length - 1];
                else
                    lps[i++] = 0;
            }
            return lps;
        }
    }
}
