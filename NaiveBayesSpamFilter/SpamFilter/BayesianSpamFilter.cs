using System;
using System.IO;
using System.Linq;
using NaiveBayesSpamFilter.Interfaces;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class BayesianSpamFilter
    {
        private const double SpamBottomBound = 0.5;
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
                .Where(probabilityProvider.HasProbability)
                .Select(word => probabilityProvider.GetProbabilityOf(MsgClass.Spam, word))
                .Sum(spamGivenWordPr => Math.Log(1 - spamGivenWordPr) - Math.Log(spamGivenWordPr));

            var spamGivenAllWordsProbability = 1/(Math.Exp(expDegree) + 1);

            return spamGivenAllWordsProbability;
        }

        public bool IsSpam(Stream mimeMessageStream)
        {
            return GetProbabilityOfSpam(mimeMessageStream) > SpamBottomBound;
        }
    }
}