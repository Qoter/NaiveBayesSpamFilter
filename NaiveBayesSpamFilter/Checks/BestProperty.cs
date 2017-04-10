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
        public void FindBestSpamWords()
        {
            var kernel = new StandardKernel(new SpamFilterModule());
            var probabilityProvider = kernel.Get<IProbabilityProvider>();

            foreach (var word in probabilityProvider.UsedWords.OrderByDescending(w => probabilityProvider.GetProbabilityOf(w, MsgClass.Spam) + 1 - probabilityProvider.GetProbabilityOf(w, MsgClass.Ham)))
            {
                Console.WriteLine($"{word} {probabilityProvider.GetProbabilityOf(word, MsgClass.Spam):0.0000} {probabilityProvider.GetProbabilityOf(word, MsgClass.Ham)}");
            }
        }
    }
}
