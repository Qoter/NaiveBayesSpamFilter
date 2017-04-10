using System;
using System.IO;
using System.Linq;
using MimeKit;
using NaiveBayesSpamFilter;
using NaiveBayesSpamFilter.Interfaces;
using NaiveBayesSpamFilter.SpamFilter;
using NHunspell;
using Ninject;

namespace NaiveBayesSpamFilter
{
    public class Program
    {
        public const string UnknownDir = @"C:\Users\Artem\Desktop\NaiveBayesSpamFilter\NaiveBayesSpamFilter\data\unknown";

        public static void Main(string[] args)
        {
            var kernel = new StandardKernel(new SpamFilterModule());
            var spamFilter = kernel.Get<BayesianSpamFilter>();



            var spamProbability = new DirectoryInfo(UnknownDir).EnumerateFiles()
                .Select(f => new { FileName = f.Name, P = spamFilter.GetProbabilityOfSpam(f.OpenRead()) })
                .OrderByDescending(f => f.P);

            var c = 1;
            foreach (var info in spamProbability)
            {
                Console.WriteLine($"{c++}. {info.P:0.0000} {info.FileName}");
            }
        }
    }
}
