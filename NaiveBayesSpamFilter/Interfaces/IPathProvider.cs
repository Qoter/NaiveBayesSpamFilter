using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesSpamFilter.Interfaces
{
    public interface IPathProvider
    {
        DirectoryInfo HamOnlyDirectory { get; }
        DirectoryInfo SpamOnlyDirectory { get; }
        DirectoryInfo UnknownDirectory { get; }
    }
}
