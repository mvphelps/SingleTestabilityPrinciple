using MathApp;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private readonly Calculator _calc = new Calculator();

        [Test]
        public void Add()
        {
           Assert.AreEqual(3.0d, _calc.Add(1,2)); 
        }

        [Test]
        public void AddDoesntHaveBinaryRepresentationProblems()
        {
            Assert.AreEqual(-6.315241m, _calc.Add(-11, 4.684759m));
        }
    }
}