using System.IO;
using NaiveBayesSpamFilter.Interfaces;
using NaiveBayesSpamFilter.SpamFilter;
using Ninject;
using NUnit.Framework;

namespace NaiveBayesSpamFilter.Checks
{
    [TestFixture]
    public class CrossCheck
    {
        [TestCase(4)]
        public void RunCrossCheck(int partsCount)
        {
            var kernel = new StandardKernel(new SpamFilterModule());
            kernel.Rebind<ITrainingSample>().To(new CustomTrainingSample())
        }

        public void CrossCheck(FileInfo[] spamFiles, FileInfo[] hamFiles)
        {
            
        }
    }
}
