using System;
using System.Runtime.InteropServices.ComTypes;
using NUnit.Framework;
using Practice31may;

namespace Tests
{
    public class StringCalculatorTests
    {
        private StringCalculator calc;

        [SetUp]
        public void Setup()
        {
            calc = new StringCalculator();
            calc.AddOccured += calc.AddCalled;
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
        public void Add_WithNegativeNumberWillThrowException_WithMessageNegativesNotAllowed()
        {
            var exception = Assert.Throws<ArgumentException>(() => calc.Add("//;\n-1;2;3"));
            Assert.AreEqual("Negatives not allowed: -1", exception.Message);
        }

        [Test]
        public void Add_WithNegativeNumbersWillThrowException_WithMessageNegativesNotAllowed_ArrayOfNegatives()
        {
            var exception = Assert.Throws<ArgumentException>(() => calc.Add("//;\n-1;-2;2;3"));
            Assert.AreEqual("Negatives not allowed: -1, -2", exception.Message);
        }

        [Test]
        public void GetCallCount_WillReturnZero()
        {
            Assert.AreEqual(0, calc.GetCalledCount());
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