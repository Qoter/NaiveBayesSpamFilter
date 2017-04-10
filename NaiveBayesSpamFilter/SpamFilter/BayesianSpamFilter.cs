using System;
using System.IO;
using System.Linq;
using NaiveBayesSpamFilter.Interfaces;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class BayesianSpamFilter
    {
        private readonly IProbabilityProvider probabilityProvider;
        private readonly IWordsExtractor wordsExtractor;
        private readonly IWordsPreprocessor wordsPreprocessor;

        public BayesianSpamFilter(IProbabilityProvider probabilityProvider, IWordsExtractor wordsExtractor, IWordsPreprocessor wordsPreprocessor)
        {
            this.probabilityProvider = probabilityProvider;
            this.wordsExtractor = wordsExtractor;
            this.wordsPreprocessor = wordsPreprocessor;
        }

        public double GetProbabilityOfSpam(Stream mimeMessageStream)
        {
            var rawWords = wordsExtractor.ExtractWords(mimeMessageStream);
            var preprocessedWords = wordsPreprocessor.PreprocessWords(rawWords);

            var expDegree = preprocessedWords
                .Where(w => probabilityProvider.Contains(w))
                .Select(word => probabilityProvider.GetProbabilitySpamGivenWord(word))
                .Sum(p => Math.Log(1 - p) - Math.Log(p));

            var probabilityOfSpamGivenWordsFromMessage = 1/(Math.Exp(expDegree) + 1);

            return probabilityOfSpamGivenWordsFromMessage;
        }
    }
}