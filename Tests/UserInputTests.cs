using MathApp;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class UserInputTests
    {
        [Test]
        public void IsBad()
        {
            var input = new UserInput("junk");
            Assert.IsTrue(input.IsBad);
            Assert.AreEqual("junk", input.RawValue);
        }
        [Test]
        public void IsQuit()
        {
            var input = new UserInput("q");
            Assert.IsTrue(input.IsQuit);
        }
        [Test]
        public void IsNumber()
        {
            var input = new UserInput("-12.454");
            Assert.IsTrue(input.IsNumber);
            Assert.AreEqual(-12.454m,input.Number);
        }
    }
}

