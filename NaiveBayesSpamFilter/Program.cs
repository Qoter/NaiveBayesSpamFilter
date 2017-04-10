using System;
using System.IO;
using System.Linq;
using Fclp;
using NaiveBayesSpamFilter.Interfaces;
using NaiveBayesSpamFilter.SpamFilter;
using Ninject;

namespace NaiveBayesSpamFilter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fclp = new FluentCommandLineParser<CustomWorkspace>();
            fclp.Setup(arg => arg.HamPath)
                .As('h', "ham")
                .WithDescription("Path to directory with ONLY HAM messages (default: data/notSpam)")
                .SetDefault(Path.Combine("data", "notSpam"));

            fclp.Setup(arg => arg.SpamPath)
                .As('s', "spam")
                .WithDescription("Path to directory with ONLY SPAM messages (default: data/spam)")
                .SetDefault(Path.Combine("data", "spam"));

            fclp.Setup(arg => arg.UnknownPath)
                .As('u', "unknown")
                .WithDescription("Path to directory with UNKNOWN messages (default: data/unknown)")
                .SetDefault(Path.Combine("data", "unknown"));

            fclp.SetupHelp("?", "help")
                .Callback(helpText => Console.WriteLine(helpText));

            var parserResult = fclp.Parse(args);

            if (parserResult.HelpCalled)
            {
                return;
            }

            if (parserResult.HasErrors)
            {
                Console.WriteLine(parserResult.ErrorText);
                return;
            }

            Run(fclp.Object);
        }

        private static void Run(IWorkspace workspace)
        {
            var kernel = new StandardKernel(new SpamFilterModule(workspace));
            var spamFilter = kernel.Get<BayesianSpamFilter>();

            var messagesSpamStat = workspace.UnknownDirectory.EnumerateFiles()
                .Select(f => new {FileName = f.Name, IsSpam = spamFilter.IsSpam(f.OpenRead())})
                .OrderByDescending(x => x.IsSpam ? 1 : 0);


            foreach (var msgStat in messagesSpamStat)
            {
                Console.WriteLine($"{msgStat.FileName};{(msgStat.IsSpam ? 1 : 0)}");
            }
        }
    }
}
