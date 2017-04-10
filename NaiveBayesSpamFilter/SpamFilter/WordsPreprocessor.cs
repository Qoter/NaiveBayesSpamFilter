using System.Collections.Generic;
using System.Linq;
using NaiveBayesSpamFilter.Interfaces;
using NHunspell;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class WordsPreprocessor : IWordsPreprocessor
    {
        private readonly Hunspell hunspell;

        public WordsPreprocessor()
        {
            hunspell = new Hunspell("en_US.aff", "en_US.dic");
        }

        public IEnumerable<string> PreprocessWords(IEnumerable<string> words)
        {
            return words
                .Select(w => w.ToLower())
                .Where(w => w.Length > 3)
                .Where(w => hunspell.Spell(w));
        }
    }
}