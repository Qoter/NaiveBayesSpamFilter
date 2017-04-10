using System.IO;

namespace NaiveBayesSpamFilter.Interfaces
{
    public interface IWorkspace
    {
        DirectoryInfo HamOnlyDirectory { get; }
        DirectoryInfo SpamOnlyDirectory { get; }
        DirectoryInfo UnknownDirectory { get; }
    }
}
