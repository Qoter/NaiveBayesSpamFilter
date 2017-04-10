using System.IO;
using NaiveBayesSpamFilter.Interfaces;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class ConstantWorkspace : IWorkspace
    {
        public DirectoryInfo SpamOnlyDirectory => new DirectoryInfo(Path.Combine("data", "spam"));
        public DirectoryInfo HamOnlyDirectory => new DirectoryInfo(Path.Combine("data", "notSpam"));
        public DirectoryInfo UnknownDirectory => new DirectoryInfo(Path.Combine("data", "unknown"));
    }
}