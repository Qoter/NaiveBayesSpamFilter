using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NaiveBayesSpamFilter.Infrastructure
{
    public static class RegexExtensions
    {
        public static IEnumerable<string> EnumerateMatches(this Regex pattern, string input)
        {
            return pattern.Matches(input).Cast<Match>().Select(x => x.ToString());
        }
    }
}