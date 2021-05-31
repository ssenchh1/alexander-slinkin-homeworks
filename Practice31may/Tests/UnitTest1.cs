using System;
using System.Runtime.InteropServices.ComTypes;
using NUnit.Framework;
using Practice31may;

namespace Tests
{
    public class Tests
    {
        private StringCalculator calc;

        [SetUp]
        public void Setup()
        {
            calc = new StringCalculator();
        }

        [Test]
        public void Add_WithEmptyLineWillReturnZero()
        {
            Assert.AreEqual(0, calc.Add(""));
        }

        [Test]
        public void Add_WithOneCharacterWillReturnCharacterItself()
        {
            Assert.AreEqual(1, calc.Add("1"));
        }

        [Test]
        public void Add_TwoOrMoreParametersWillReturnTheirSum()
        {
            Assert.AreEqual(3, calc.Add("1, 2"));
        }

        [Test]
        public void Add_WithNewDelimiterWillReturnSix()
        {
            Assert.AreEqual(6, calc.Add("1\n2,3"));
        }

        [Test]
        public void Add_WithCustomDelimiterWillWorkCorrectly()
        {
            Assert.AreEqual(6, calc.Add("//;\n1;2;3"));
        }

        [Test]
        public void Add_WithNegativeNumberWillThrowException()
        {
            Assert.Throws(typeof(ArgumentException), () => calc.Add("//;\n-1;2;3"));
        }

        [Test]
        public void GetCallCount_WillReturnSix()
        {
            Assert.AreEqual(6, calc.GetCalledCount());
        }

        [Test]
        public void Add_NumbersBiggerThan1000WillBeIgnored()
        {
            Assert.AreEqual(3, calc.Add("1, 2, 1000"));
        }

        [Test]
        public void Add_BigDelimiterWillWorkCorrectlyAndReturnSix()
        {
            Assert.AreEqual(6, calc.Add("//****\n1****2****3"));
        }

        [Test]
        public void Add_TwoCustomDelimitersWillReturnSix()
        {
            Assert.AreEqual(6, calc.Add("//[**][;;]\n1**2;;3"));
        }
    }
}