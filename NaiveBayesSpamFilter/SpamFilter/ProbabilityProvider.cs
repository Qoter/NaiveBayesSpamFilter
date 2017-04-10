using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NaiveBayesSpamFilter.Interfaces;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class ProbabilityProvider : IProbabilityProvider
    {
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

        public double GetProbabilityOf(string word, MsgClass givenMsgClass)
        {
            if (!HasProbability(word))
                throw new ArgumentException("Word not found");

            return givenMsgClass == MsgClass.Spam
                ? spamCountWithWord[word]/(double) totalSpamCount
                : hamCountWithWord[word]/(double) totalHamCount;
        }

        public double GetProbabilityOf(MsgClass msgClass, string givenWord)
        {
            if (!HasProbability(givenWord))
                throw new ArgumentException("Word not found");

            return GetProbabilityOf(givenWord, msgClass)*GetProbabilityOf(msgClass)/GetProbabilityOf(givenWord);
        }

        public double GetProbabilityOf(MsgClass msgClass)
        {
            return msgClass == MsgClass.Spam ? 0.47 : 1 - 0.47;
        }

        public double GetProbabilityOf(string word)
        {
            return GetProbabilityOf(word, MsgClass.Spam)*GetProbabilityOf(MsgClass.Spam) +
                   GetProbabilityOf(word, MsgClass.Ham)*GetProbabilityOf(MsgClass.Ham);
        }

        public bool HasProbability(string word)
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