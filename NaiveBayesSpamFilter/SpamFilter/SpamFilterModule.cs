using NaiveBayesSpamFilter.Interfaces;
using Ninject.Modules;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class SpamFilterModule : NinjectModule
    {
        private readonly IWorkspace workspace;

        public SpamFilterModule(IWorkspace workspace=null)
        {
            this.workspace = workspace ?? new ConstantWorkspace();
        }

        public override void Load()
        {
            Bind<IWorkspace>().ToConstant(workspace);
            Bind<ITrainingSample>().To<WorksapaceTrainigSample>();
            Bind<IProbabilityProvider>().To<ProbabilityProvider>();
            Bind<IWordsExtractor>().To<WordsExtractor>();
            Bind<IWordsPreprocessor>().To<WordsPreprocessor>().InSingletonScope();
        }
    }
}
