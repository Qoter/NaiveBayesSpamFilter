using System.Collections.Generic;
using System.IO;
using NaiveBayesSpamFilter.Interfaces;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class FoldersTrainigSample : ITrainingSample
    {
        public IEnumerable<FileInfo> SpamFiles => spamDirectory.EnumerateFiles();
        public IEnumerable<FileInfo> HamFiles => hamDirectory.EnumerateFiles();

        private readonly DirectoryInfo spamDirectory;
        private readonly DirectoryInfo hamDirectory;

        public FoldersTrainigSample(string spamDirectory, string hamDirectory)
        {
            this.spamDirectory = new DirectoryInfo(spamDirectory);
            this.hamDirectory = new DirectoryInfo(hamDirectory);
        }
    }
}