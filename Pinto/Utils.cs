using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoNS
{
    // Why the fuck was I retarded and put these in Program.cs????
    public static class Utils
    {
        public static IEnumerable<string> SplitStringIntoChunks(string str, int chunkSize)
        {
            for (int i = 0; i < str.Length; i += chunkSize)
                yield return str.Substring(i, Math.Min(chunkSize, str.Length - i));
        }

        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict,
            TKey key, TValue @default)
        {
            return dict.TryGetValue(key, out var value) ? value : @default;
        }
    }
}
