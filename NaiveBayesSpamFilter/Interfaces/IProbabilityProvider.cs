namespace NaiveBayesSpamFilter.Interfaces
{
    public interface IProbabilityProvider
    {
        //double GetConditionalProbabilityOf(string word, MsgClass givenMsgClass);
        //double GetConditionalProbabilityOf(MsgClass msgClass, string givenWord);
        //double GetProbabilityOf(MsgClass msgClass);

        double GetProbabilityWordGivenSpam(string word);
        double GetProbabilityWordGivenHam(string word);
        bool Contains(string word);
    }
}