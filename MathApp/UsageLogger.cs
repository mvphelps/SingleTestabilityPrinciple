using System.IO;

namespace MathApp
{
    public interface IUsageLogger
    {
        void RecordSuccess(decimal addend1, decimal addend2, decimal result);
        void RecordError(string userInput);
    }

    public class UsageLogger : IUsageLogger
    {
        private readonly string _fileName;

        public UsageLogger(string fileName = "logfile.txt")
        {
            _fileName = fileName;
        }

        public void RecordSuccess(decimal addend1, decimal addend2, decimal result)
        {
            File.AppendAllText(_fileName, $"S: {addend1}+{addend2}={result}\r\n");
        }

        public void RecordError(string userInput)
        {
            File.AppendAllText(_fileName, $"F: {userInput}\r\n");
        }
    }
}