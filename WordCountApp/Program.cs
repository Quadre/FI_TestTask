using System;
using System.Collections.Generic;
using WordCounterCore;

namespace WordCountApp
{
    class Program
    {
        static int Main(string[] args)
        {
            string sentence = "";
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a sentence to parse:");
                sentence = Console.ReadLine();
            }
            else if (args.Length == 1)
            {
                sentence = args[0];
            }
            else
            {
                Console.WriteLine("Only one argument is expected.\nExample: WordCountApp.exe \"This is a statement, and so is this.\" ");
                return -1;   
            }

            IWordCounter wc = new WordCounter();
            WordCounterDic actResult = null;
            if (wc.TryCalculate(sentence, out actResult))
            {
                foreach (KeyValuePair<string, uint> item in actResult)
                {
                    Console.WriteLine("{0} - {1}", item.Key, item.Value);
                }

                return 0;
            }
            else
            {
                Console.WriteLine("'{0}' is not a valid sentece.", sentence);
                return -1;
            }            
        }
    }
}
