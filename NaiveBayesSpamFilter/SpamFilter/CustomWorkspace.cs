using System.IO;
using NaiveBayesSpamFilter.Interfaces;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class CustomWorkspace : IWorkspace
    {
        public DirectoryInfo HamOnlyDirectory => new DirectoryInfo(HamPath);
        public DirectoryInfo SpamOnlyDirectory => new DirectoryInfo(SpamPath);
        public DirectoryInfo UnknownDirectory => new DirectoryInfo(UnknownPath);

        public string HamPath { get; set; }
        public string SpamPath { get; set; }
        public string UnknownPath { get; set; }
    }
}
