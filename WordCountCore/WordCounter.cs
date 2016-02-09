using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace WordCounterCore
{
    public class WordCounter : IWordCounter
    {
        public static string[] ExcludedPatterns = new string[] {".", ",", ":", ";", "!", "?" };        

        public WordCounterDic Calculate(string input)
        {
            WordCounterDic result = new WordCounterDic();
            string[] arr = NormalizeString(input).Split(' ');            
            foreach (string item in arr)
            {                
                if (!result.ContainsKey(item))
                {
                    string[] words = Array.FindAll(arr, s => s == item);
                    result.Add(item, (uint)words.Length);
                }
            }
            return result;
        }        

        public bool TryCalculate(string input, out WordCounterDic result)
        {
            result = null;
            string str = NormalizeString(input);

            if (str.Length == 0)
            {
                return false;
            }

            try
            {
                result = Calculate(str);
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
            Regex newReg = new Regex(notPrinted, RegexOptions.Compiled);            
            input = newReg.Replace(input, " ");

            return input.Trim();
        }
    }
}
