using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace WordCounterCore
{
    public class WordCounter : IWordCounter
    {
        public static string[] ExcludedPatterns = new string[] { ".", ",", ":", ";", "!", "?" };

        public Dictionary<string, uint> Calculate(string input)
        {
            string str = NormalizeString(input);
            if (str.Length == 0)
            {
                throw new ArgumentException("Provided string is not a valid sentence.");
            }
            
            var result = str.Split(' ')
                .GroupBy(c => c)
                .Select(x => new { word = x.Key, count = (uint) x.Count() })
                .ToDictionary(p => p.word, x => x.count);

            return result;
        }

        public bool TryCalculate(string input, out Dictionary<string, uint> result)
        {
            result = null;

            try
            {
                result = Calculate(input);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string NormalizeString(string input)
        {
            input = input.ToLower();
            

            foreach (string item in ExcludedPatterns)
            {
                input = input.Replace(item, "");
            }

            // replace all doubled not-printed chars (including double spaces) with ne space
            string notPrinted = @"\s+";
            var newReg = new Regex(notPrinted, RegexOptions.Compiled);
            input = newReg.Replace(input, " ");

            return input.Trim();
        }        
    }
}
