using System.Collections.Generic;
using System.IO;
using NaiveBayesSpamFilter.Interfaces;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class FoldersTrainigSample : ITrainingSample
    {
        public IEnumerable<FileInfo> SpamFiles => workspace.SpamOnlyDirectory.EnumerateFiles();
        public IEnumerable<FileInfo> HamFiles => workspace.HamOnlyDirectory.EnumerateFiles();

        private readonly IWorkspace workspace;

        public FoldersTrainigSample(IWorkspace workspace)
        {
            this.workspace = workspace;
        }
    }
}