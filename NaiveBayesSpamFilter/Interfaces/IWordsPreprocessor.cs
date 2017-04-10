using System.Collections.Generic;

namespace NaiveBayesSpamFilter.Interfaces
{
    public interface IWordsPreprocessor
    {
        IEnumerable<string> PreprocessWords(IEnumerable<string> words);
    }
}