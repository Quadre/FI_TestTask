using System;
using System.Collections.Generic;

namespace WordCounterCore
{
    public class WordCounterDic : Dictionary<string, uint> { }


    public interface IWordCounter
    {
        WordCounterDic Calculate(string input);
        bool TryCalculate(string input, out WordCounterDic result);
    }
}
