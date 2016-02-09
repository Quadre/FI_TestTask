using System;
using WordCounterCore;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests
{
    [TestFixture]
    public class Test
    {
        private IWordCounter wc;

        [OneTimeSetUp]
        public void SetUp()
        {
            wc = new WordCounter();
        }


        static object[] staticDataPositive =
        {
            new object[] { "Yes!", new Dictionary<string, uint>() { { "yes", 1 } } },
            new object[] { "This is a statement, and so is this.", new Dictionary<string, uint>() {
                { "this", 2 },
                { "is", 2 },
                { "a", 1 },
                { "statement", 1 },
                { "and", 1 },
                { "so", 1 }
                } },
            new object[] { "Ok! ok 1   R2D2 \nBuy-buy", new Dictionary<string, uint>() {
                { "ok", 2},                
                { "1", 1},
                { "r2d2", 1},
                { "buy-buy", 1},
                } },            
        };

        static object[] staticDataNegative =
        {
            new object[] { "", null},
            new object[] { " ... ", null},
            new object[] { " . , :! !? ? ! : ;", null}            
        };

        [Test, 
         TestCaseSource("staticDataPositive"),
         Description ("Check that Calculate method works correct with proper sentence.")]
        public void Calculate(string sentence, Dictionary<string, uint> expResult)
        {            
            Dictionary<string, uint> actResult = wc.Calculate(sentence);            
            CollectionAssert.AreEquivalent(expResult, actResult, "Expexted and Actual Result dictionary are not match for sentence:\n" + sentence);            
        }
        
        [Test,
         TestCaseSource("staticDataNegative"),
         Description("Check that Calculate method throws ArgumentException, when in-proper sentence are passed.")]
        public void CalculateThrowException(string sentence, Dictionary<string, uint> expResult)
        {            
            Assert.Throws<ArgumentException> (() => wc.Calculate(sentence), "Calculate should throw exception [ArgumentException] when colling with inproper strings. Sentence:\n" + sentence);
        }


        [Test,
         TestCaseSource("staticDataNegative"),
         Description("Check that TryCalculate method return false, when in-proper sentence are passed.")]
        public void TryCalculateIsFalse(string sentence, Dictionary<string, uint> expResult)
        {
            Dictionary<string, uint> actResult = null;
            Assert.IsFalse(wc.TryCalculate(sentence, out actResult), "TryCalculate should fail with sentence:\n" + sentence);
        }

        [Test,
         TestCaseSource("staticDataPositive"),
         Description("Check that TryCalculate method return true, when proper sentence are passed.")]
        public void TryCalculateIsTrue(string sentence, Dictionary<string, uint> expResult)
        {
            Dictionary<string, uint> actResult = null;
            Assert.IsTrue(wc.TryCalculate(sentence, out actResult), "TryCalculate should pass with sentence:\n" + sentence);            
        }

        [Test,
         TestCaseSource("staticDataNegative"),
         Description("Check that TryCalculate method doesn't throw exception, when in-proper sentence are passed.")]
        public void TryCalculateNoException(string sentence, Dictionary<string, uint> expResult)
        {
            Dictionary<string, uint> actResult = null;            
            Assert.DoesNotThrow(() => wc.TryCalculate(sentence, out actResult), "TryCalculate should be exception safe method. Sentence:\n" + sentence);
        }

        [Test,
         TestCaseSource("staticDataPositive"),
         Description("Check that TryCalculate method works correct with proper sentence.")]
        public void TryCalculate(string sentence, Dictionary<string, uint> expResult)
        {
            Dictionary<string, uint> actResult = null;
            wc.TryCalculate(sentence, out actResult);
            CollectionAssert.AreEquivalent(expResult, actResult, "Expexted and Actual Result dictionary are not match for sentence:\n" + sentence);

        }
    }
}
