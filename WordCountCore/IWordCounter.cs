using System;
using System.Collections.Generic;

namespace WordCounterCore
{
    public interface IWordCounter
    {
        Dictionary<string, uint> Calculate(string input);
        bool TryCalculate(string input, out Dictionary<string, uint> result);
    }
}
