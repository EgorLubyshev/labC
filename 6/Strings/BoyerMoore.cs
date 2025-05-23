using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strings
{
    public class BoyerMooreAlgorithm : ISubstringSearch
    {
        public List<int> IndexesOf(string pattern, string text)
        {
            var result = new List<int>();
            if (pattern.Length > text.Length) return result;

            int[] badChar = new int[256];
            for (int i = 0; i < 256; i++) badChar[i] = -1;
            for (int i = 0; i < pattern.Length; i++)
                badChar[(int)pattern[i]] = i;

            int s = 0;
            while (s <= text.Length - pattern.Length)
            {
                int j = pattern.Length - 1;
                while (j >= 0 && pattern[j] == text[s + j])
                    j--;

                if (j < 0)
                {
                    result.Add(s);
                    s += (s + pattern.Length < text.Length) ? pattern.Length - badChar[text[s + pattern.Length]] : 1;
                }
                else
                    s += Math.Max(1, j - badChar[text[s + j]]);
            }

            return result;
        }
    }
}
