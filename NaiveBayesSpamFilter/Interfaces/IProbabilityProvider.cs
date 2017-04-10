using System.Collections.Generic;
using NaiveBayesSpamFilter.SpamFilter;

namespace NaiveBayesSpamFilter.Interfaces
{
    public interface IProbabilityProvider
    {
        double GetProbabilityOf(string word, MsgClass givenMsgClass);
        double GetProbabilityOf(MsgClass msgClass, string givenWord);
        double GetProbabilityOf(MsgClass msgClass);
        double GetProbabilityOf(string word);
        bool HasProbability(string word);
        IEnumerable<string> UsedWords { get; }
    }
}