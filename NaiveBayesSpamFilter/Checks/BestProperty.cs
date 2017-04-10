using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveBayesSpamFilter.Interfaces;
using NaiveBayesSpamFilter.SpamFilter;
using Ninject;
using NUnit.Framework;

namespace NaiveBayesSpamFilter.Checks
{
    [TestFixture]
    public class BestProperty
    {
        public BestProperty()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        [Test]
        public void FindMostSignificantWords()
        {
            var kernel = new StandardKernel(new SpamFilterModule());
            var probabilityProvider = kernel.Get<IProbabilityProvider>();

            var stats = probabilityProvider.UsedWords
                .Select(
                    w =>
                        new
                        {
                            Word = w,
                            SpamGivenWordPr = probabilityProvider.GetProbabilityOf(MsgClass.Spam, w),
                            HamGivenWordPr = probabilityProvider.GetProbabilityOf(MsgClass.Ham, w)
                        })
                .OrderByDescending(x => Math.Max(x.SpamGivenWordPr/x.HamGivenWordPr, x.HamGivenWordPr/x.SpamGivenWordPr));

            foreach (var stat in stats)
            {
                Console.WriteLine($"{stat.Word} {stat.SpamGivenWordPr:0.0000} {stat.HamGivenWordPr:0.0000}");
            }
        }
    }
}
