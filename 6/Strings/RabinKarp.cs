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

        public List<int> IndexesOf(string pattern, string text)
        {
            var result = new List<int>();
            if (pattern.Length > text.Length) return result;

            long patternHash = CreateHash(pattern, pattern.Length);
            long textHash = CreateHash(text, pattern.Length);

            for (int i = 0; i <= text.Length - pattern.Length; i++)
            {
                if (patternHash == textHash && text.Substring(i, pattern.Length) == pattern)
                    result.Add(i);
                if (i < text.Length - pattern.Length)
                    textHash = RecalculateHash(text, i, pattern.Length, textHash);
            }

            return result;
        }

        private long CreateHash(string str, int length)
        {
            long hash = 0;
            for (int i = 0; i < length; i++)
                hash += str[i] * (long)Math.Pow(Prime, i);
            return hash;
        }

        private long RecalculateHash(string str, int index, int patternLen, long oldHash)
        {
            long newHash = oldHash - str[index];
            newHash /= Prime;
            newHash += str[index + patternLen] * (long)Math.Pow(Prime, patternLen - 1);
            return newHash;
        }
    }
}
