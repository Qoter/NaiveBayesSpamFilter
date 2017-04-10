using NaiveBayesSpamFilter.Interfaces;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public static class ProbabilityProviderExtensions
    {
        public static double GetProbabilitySpamGivenWord(this IProbabilityProvider probabilityProvider, string word)
        {
            var pWordGivenSpam = probabilityProvider.GetProbabilityWordGivenSpam(word);
            var pWordGivenHam = probabilityProvider.GetProbabilityWordGivenHam(word);

            //TODO probability of spam message = 0.5
            return pWordGivenSpam/(pWordGivenSpam + pWordGivenHam);
        }

        public static double GetProbabilityHamGivenWord(this IProbabilityProvider probabilityProvider, string word)
        {
            var pWordGivenSpam = probabilityProvider.GetProbabilityWordGivenSpam(word);
            var pWordGivenHam = probabilityProvider.GetProbabilityWordGivenHam(word);

            //TODO probability of spam message = 0.5
            return pWordGivenHam /(pWordGivenSpam + pWordGivenHam);
        }
    }
}