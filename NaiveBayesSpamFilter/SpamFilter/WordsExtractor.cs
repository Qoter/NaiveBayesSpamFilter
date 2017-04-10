using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MimeKit;
using NaiveBayesSpamFilter.Infrastructure;
using NaiveBayesSpamFilter.Interfaces;

namespace NaiveBayesSpamFilter.SpamFilter
{
    public class WordsExtractor : IWordsExtractor
    {
        private static readonly Regex WordPattern = new Regex("[A-Za-z]+", RegexOptions.Compiled);

        public IEnumerable<string> ExtractWords(Stream mimeMessageStream)
        {
            var msg = MimeMessage.Load(mimeMessageStream);

            var subjectWords = WordPattern.EnumerateMatches(msg.Subject ?? "");
            var bodyWords = WordPattern.EnumerateMatches(msg.HtmlBody ?? msg.TextBody ?? "");

            return subjectWords.Concat(bodyWords);
        }
    }
}