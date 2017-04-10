using System.Collections.Generic;
using System.IO;
using NaiveBayesSpamFilter.Interfaces;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class CustomTrainingSample : ITrainingSample
    {
        public IEnumerable<FileInfo> SpamFiles { get; }
        public IEnumerable<FileInfo> HamFiles { get; }

        public CustomTrainingSample(List<FileInfo> spamFiles, List<FileInfo> hamFiles)
        {
            SpamFiles = spamFiles;
            HamFiles = hamFiles;
        }
    }
}
