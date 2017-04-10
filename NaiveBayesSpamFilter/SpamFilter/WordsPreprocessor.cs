using System.Collections.Generic;
using System.Linq;
using NaiveBayesSpamFilter.Interfaces;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class WordsPreprocessor : IWordsPreprocessor
    {
        public WordsPreprocessor()
        {
        }

        public IEnumerable<string> PreprocessWords(IEnumerable<string> words)
        {
            return words
                .Select(w => w.ToLower());
        }
    }
}