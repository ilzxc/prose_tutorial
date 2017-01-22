using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace prosest.Semantics
{
    public static class Semantics
    {
        static string Substring(string inp, Tuple<int?, int?> posPair)
        {
            if (posPair.Item1 == null || posPair.Item2 == null)
                return null;
            int start = posPair.Item1.Value;
            int end = posPair.Item2.Value;
            if (start < 0 || start >= inp.Length ||
                end < 0 || end >= inp.Length || end < start)
                return null;
            return inp.Substring(start, end - start);
        }

        static int? AbsolutePosition(string inp, int k)
        {
            if (k > inp.Length || k < (-inp.Length - 1))
                return null;
            return k >= 0 ? k : (inp.Length + k + 1);
        }

        static int? RegexPosition(string inp, Tuple<Regex, Regex> regexPair, int occurrence)
        {
            if (regexPair.Item1 == null || regexPair.Item2 == null)
                return null;
            Regex left = regexPair.Item1;
            Regex right = regexPair.Item2;
            var rightMatches = right.Matches(inp).Cast<Match>().ToDictionary(m => m.Index);
            var matchPositions = new List<int>();
            foreach (Match m in left.Matches(inp))
            {
                if (rightMatches.ContainsKey(m.Index + m.Length))
                    matchPositions.Add(m.Index + m.Length);
            }
            if (occurrence >= matchPositions.Count ||
                occurrence < -matchPositions.Count)
                return null;
            return occurrence >= 0
                ? matchPositions[occurrence]
                : matchPositions[matchPositions.Count + occurrence];
        }
    }
}
