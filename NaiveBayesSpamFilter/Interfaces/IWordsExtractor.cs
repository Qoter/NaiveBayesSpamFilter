using System.Collections.Generic;
using System.IO;

namespace NaiveBayesSpamFilter.Interfaces
{
    public interface IWordsExtractor
    {
        IEnumerable<string> ExtractWords(Stream mimeMessageStream);
    }
}