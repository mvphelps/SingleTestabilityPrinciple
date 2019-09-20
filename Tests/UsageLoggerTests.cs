using System.IO;
using MathApp;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class UsageLoggerTests
    {
        private string _logFileName = "testlog.txt";
        private IUsageLogger _usageLogger;

        [SetUp]
        public void SetUp()
        {
            Teardown();
            _usageLogger = new UsageLogger(_logFileName);
        }

        [TearDown]
        public void Teardown()
        {
            if (File.Exists(_logFileName))
            {
                File.Delete(_logFileName);
            }
        }

        private void AssertFileHasContents(string expected)
        {
            var actualContents = File.ReadAllText(_logFileName);
            Assert.AreEqual(expected, actualContents);
        }

        [Test]
        public void LogError()
        {
            _usageLogger.RecordError("bad things happened");

            AssertFileHasContents("F: bad things happened\r\n");
        }

        [Test]
        public void LogSuccess()
        {
            _usageLogger.RecordSuccess(1, 2, 3);

            AssertFileHasContents("S: 1+2=3\r\n");
        }
    }

    
}