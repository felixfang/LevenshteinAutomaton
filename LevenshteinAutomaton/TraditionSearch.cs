using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LevenshteinAutomaton
{
    public class TraditionSearch
    {
        public static IEnumerable<string> search(string oriword, double factor, IEnumerator<string> it)
        {
            List<string> results = new List<string>();
            while (it.MoveNext())
            {
                int lenght = Math.Max(oriword.Length, it.Current.Length);
                int dist = (int)(lenght * (1 - factor));
                if (it.Current.Length > 0 && calDistance(oriword, it.Current) <= dist)
                {
                    results.Add(it.Current);
                }
            }
            return results;
        }

        public static IEnumerable<string> search(string oriword, int dist, IEnumerator<string> it)
        {
            List<string> results = new List<string>();
            while (it.MoveNext())
            {
                if (it.Current.Length > 0 && calDistance(oriword, it.Current) <= dist)
                {
                    results.Add(it.Current);
                }
            }
            return results;
        }

        private static int calDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;

            int[,] d = new int[n + 1, m + 1];

            int cost;

            if (n == 0) return m;
            if (m == 0) return n;

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    cost = (t[j-1] == s[i-1] ? 0 : 1);
                    d[i, j] = System.Math.Min(System.Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                              d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }
    }
}
