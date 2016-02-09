﻿using System;
using WordCounterCore;
using NUnit.Framework;

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
            new object[] { "Yes!", new WordCounterDic() { { "yes", 1 } } },
            new object[] { "This is a statement, and so is this.", new WordCounterDic() {
                { "this", 2 },
                { "is", 2 },
                { "a", 1 },
                { "statement", 1 },
                { "and", 1 },
                { "so", 1 }
                } },
            new object[] { "Ok! ok 1   R2D2 \nBuy-buy", new WordCounterDic() {
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

        [Test, TestCaseSource("staticDataPositive")]
        public void Calculate(string sentence, WordCounterDic expResult)
        {            
            WordCounterDic actResult = wc.Calculate(sentence);            
            CollectionAssert.AreEquivalent(expResult, actResult, "Expexted and Actual Result dictionary are not match for sentence:\n" + sentence);            
        }

        [Test, TestCaseSource("staticDataNegative")]
        public void TryCalculateIsFalse(string sentence, WordCounterDic expResult)
        {
            WordCounterDic actResult = null;
            Assert.IsFalse(wc.TryCalculate(sentence, out actResult), "TryCalculate should fail with sentence:\n" + sentence);
        }

        [Test, TestCaseSource("staticDataPositive")]
        public void TryCalculateIsTrue(string sentence, WordCounterDic expResult)
        {
            WordCounterDic actResult = null;
            Assert.IsTrue(wc.TryCalculate(sentence, out actResult), "TryCalculate should pass with sentence:\n" + sentence);            
        }

        [Test, TestCaseSource("staticDataPositive")]
        public void TryCalculate(string sentence, WordCounterDic expResult)
        {
            WordCounterDic actResult = null;
            wc.TryCalculate(sentence, out actResult);
            CollectionAssert.AreEquivalent(expResult, actResult, "Expexted and Actual Result dictionary are not match for sentence:\n" + sentence);

        }
    }
}
