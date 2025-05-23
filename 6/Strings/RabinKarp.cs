using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strings
{
    public class RabinKarpAlgorithm : ISubstringSearch
    {
        private const int Prime = 101;
        private const int Mod = 1000000007;  

        public List<int> IndexesOf(string pattern, string text)
        {
            var result = new List<int>();
            int m = pattern.Length, n = text.Length;
            if (m > n) return result;

            long patternHash = CreateHash(pattern, m);
            long textHash = CreateHash(text, m);
            long highestPow = 1;

            // precompute Prime^(m-1) % Mod
            for (int i = 0; i < m - 1; i++)
                highestPow = (highestPow * Prime) % Mod;

            for (int i = 0; i <= n - m; i++)
            {
                if (patternHash == textHash && text.Substring(i, m) == pattern)
                    result.Add(i);

                if (i < n - m)
                {
                    textHash = RecalculateHash(text, i, m, textHash, highestPow);
                }
            }

            return result;
        }

        private long CreateHash(string str, int length)
        {
            long hash = 0;
            for (int i = 0; i < length; i++)
            {
                hash = (hash * Prime + str[i]) % Mod;
            }
            return hash;
        }

        private long RecalculateHash(string str, int index, int patternLen, long oldHash, long highestPow)
        {
            long newHash = oldHash - (str[index] * highestPow) % Mod;
            if (newHash < 0)
                newHash += Mod;
            newHash = (newHash * Prime + str[index + patternLen]) % Mod;
            return newHash;
        }
    }

}
