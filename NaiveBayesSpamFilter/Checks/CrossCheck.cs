using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NaiveBayesSpamFilter.Interfaces;
using NaiveBayesSpamFilter.SpamFilter;
using Ninject;
using NUnit.Framework;
using FakeItEasy;

namespace NaiveBayesSpamFilter.Checks
{
    [TestFixture]
    public class CrossCheck
    {
        public CrossCheck()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        [Test]
        public void RunCrossCheck()
        {
            var defaultWorkspace = new ConstantWorkspace();
            var spamFiles = defaultWorkspace.SpamOnlyDirectory.EnumerateFiles().ToArray();
            var hamFiles = defaultWorkspace.HamOnlyDirectory.EnumerateFiles().ToArray();

            const int partsCount = 4;

            var spams = Patition(spamFiles, partsCount).ToArray();
            var hams = Patition(hamFiles, partsCount).ToArray();

            var sumPercentSpamInSpam = 0.0;
            var sumPercentSpamInHam = 0.0;

            for (var spamForTestIndex = 0; spamForTestIndex < partsCount; spamForTestIndex++)
            {
                for (var hamForTestIndex = 0; hamForTestIndex < partsCount; hamForTestIndex++)
                {
                    var trainingSpam = spams.Where((_, i) => i != spamForTestIndex).SelectMany(x => x).ToArray();
                    var trainingHam = hams.Where((_, i) => i != hamForTestIndex).SelectMany(x => x).ToArray();

                    var percentSpamInSpam = GetPercentOfSpam(trainingSpam, trainingHam, spams[spamForTestIndex]);
                    var percentSpamInHam = GetPercentOfSpam(trainingSpam, trainingHam, hams[hamForTestIndex]);

                    Console.WriteLine($"Test: ({spamForTestIndex}, {hamForTestIndex}) Result: ({percentSpamInSpam}, {percentSpamInHam})");

                    sumPercentSpamInSpam += percentSpamInSpam;
                    sumPercentSpamInHam  += percentSpamInHam;
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Avarage spam in SPAM:{sumPercentSpamInSpam / (partsCount*partsCount)}");
            Console.WriteLine($"Avarage spam in HAM:{sumPercentSpamInHam/ (partsCount*partsCount)}");
        }

        public static IEnumerable<T[]> Patition<T>(T[] source, int partsCount)
        {
            if (source.Length < partsCount)
                throw new ArgumentException();

            var partLength = source.Length/partsCount;

            for (var partIndex = 0; partIndex < partsCount-1; partIndex++)
            {
                yield return source.Skip(partIndex*partLength).Take(partLength).ToArray();
            }
            yield return source.Skip((partsCount - 1)*partLength).ToArray();
        }

        public double GetPercentOfSpam(FileInfo[] spamFilesForTrain, FileInfo[] hamFilesForTrain, FileInfo[] filesForTest)
        {
            var filter = CreateFilter(spamFilesForTrain, hamFilesForTrain);

            var spamCount = filesForTest.Count(f => filter.IsSpam(f.OpenRead()));
            var totalCount = filesForTest.Length;

            return spamCount/(double) totalCount;
        }

        public BayesianSpamFilter CreateFilter(FileInfo[] spamFiles, FileInfo[] hamFiles)
        {
            var kernel = new StandardKernel(new SpamFilterModule());

            var trainingSample = A.Fake<ITrainingSample>();
            A.CallTo(() => trainingSample.SpamFiles).Returns(spamFiles);
            A.CallTo(() => trainingSample.HamFiles).Returns(hamFiles);

            kernel.Rebind<ITrainingSample>().ToConstant(trainingSample);

            return kernel.Get<BayesianSpamFilter>();
        }
    }
}
