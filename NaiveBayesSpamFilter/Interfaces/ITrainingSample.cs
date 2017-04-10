using System.Collections.Generic;
using System.IO;

namespace NaiveBayesSpamFilter.Interfaces
{
    public interface ITrainingSample
    {
        IEnumerable<FileInfo> SpamFiles { get; }
        IEnumerable<FileInfo> HamFiles { get; }
    }
}