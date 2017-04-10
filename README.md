## Naive Bayes spam filter

### How run?
```
$ NaiveBayesSpamFilter.exe -h <hamDirectory> -s <spamDirectory> -u <unknownDirectory>
```
__hamDirectory__ - path to directory with only ham messages.  
__spamDirectory__ - path to directory with only spam messages.  
__unknownDirectory__ - path to directory with unknown (spam or not) messages.  

### Results
Program print to __stdout__ strings: {filename}:{1 if is_spam(filename) else 0}
