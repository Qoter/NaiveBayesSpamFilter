using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NaiveBayesSpamFilter.Interfaces;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class ProbabilityProvider : IProbabilityProvider
    {
        public const double ProbabilityOfSpamMessage = 0.5;
        public const double ProbabilityOfHamMessage = 1 - ProbabilityOfSpamMessage;

        private readonly Dictionary<string, int> spamCountWithWord;
        private readonly Dictionary<string, int> hamCountWithWord;
        private readonly int totalSpamCount;
        private readonly int totalHamCount;

        public ProbabilityProvider(ITrainingSample trainingSample, IWordsExtractor wordsExtractor, IWordsPreprocessor wordsPreprocessor)
        {
            spamCountWithWord = CalculateWordsCount(trainingSample.SpamFiles, wordsExtractor, wordsPreprocessor);
            totalSpamCount = trainingSample.SpamFiles.Count();

            hamCountWithWord = CalculateWordsCount(trainingSample.HamFiles, wordsExtractor, wordsPreprocessor);
            totalHamCount = trainingSample.HamFiles.Count();

        }

        public double GetProbabilityWordGivenSpam(string word)
        {
            if (!Contains(word))
                throw new ArgumentException("Word not found");

            return spamCountWithWord[word]/(double) totalHamCount;
        }

        public double GetProbabilityWordGivenHam(string word)
        {
            if (!Contains(word))
                throw new ArgumentException("Word not found");

            return hamCountWithWord[word]/(double) totalSpamCount;
        }

        public bool Contains(string word)
        {
            return spamCountWithWord.ContainsKey(word) &&
                   hamCountWithWord.ContainsKey(word);
        }

        private static Dictionary<string, int> CalculateWordsCount(IEnumerable<FileInfo> msgFiles, IWordsExtractor wordsExtractor, IWordsPreprocessor wordsPreprocessor)
        {
            var wordToCountMessagesWithThisWord = new Dictionary<string, int>();

            foreach (var messageFile in msgFiles)
            {
                var rawWords = wordsExtractor.ExtractWords(messageFile.OpenRead());
                var preprocessedWords = wordsPreprocessor.PreprocessWords(rawWords);

                foreach (var word in preprocessedWords.Distinct())
                {
                    if (!wordToCountMessagesWithThisWord.ContainsKey(word))
                        wordToCountMessagesWithThisWord[word] = 0;
                    wordToCountMessagesWithThisWord[word]++;
                }
            }

            return wordToCountMessagesWithThisWord;
        }
    }
}