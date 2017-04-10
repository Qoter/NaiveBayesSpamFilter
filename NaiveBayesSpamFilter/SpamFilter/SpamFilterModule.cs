using NaiveBayesSpamFilter.Interfaces;
using Ninject.Modules;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class SpamFilterModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWorkspace>().To<ConstantWorkspace>();
            Bind<ITrainingSample>().To<FoldersTrainigSample>();
            Bind<IProbabilityProvider>().To<ProbabilityProvider>();
            Bind<IWordsExtractor>().To<WordsExtractor>();
            Bind<IWordsPreprocessor>().To<WordsPreprocessor>().InSingletonScope();
        }
    }
}
