using NUnit.Framework;
using CalculatorLibrary;
using System;

namespace CalculatorLibrary.Tests
{
    public class CalculatorTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Add_ShouldReturnCorrectSum()
        {
            var result = _calculator.Add(5, 3);
            Assert.That(result, Is.EqualTo(8));
        }

        [Test]
        public void Subtract_ShouldReturnCorrectDifference()
        {
            var result = _calculator.Subtract(10, 4);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Multiply_ShouldReturnCorrectProduct()
        {
            var result = _calculator.Multiply(4, 5);
            Assert.That(result, Is.EqualTo(20));
        }

        [Test]
        public void Divide_ShouldReturnCorrectQuotient()
        {
            var result = _calculator.Divide(10, 2);
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Divide_ByZero_ShouldThrowException()
        {
            Assert.Throws<DivideByZeroException>(() =>
                _calculator.Divide(10, 0));
        }

        [Test]
        public void Add_WithZero_ShouldReturnSameNumber()
        {
            var result = _calculator.Add(7, 0);
            Assert.That(result, Is.EqualTo(7));
        }
    }
}