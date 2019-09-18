namespace MathApp
{
    public interface IView
    {
        void Warn(string message);
        string RequestNumber();
        void ShowResult(string result);
        void Quit();
    }

    public class Presenter
    {
        private readonly IView _view;
        private readonly ICalculator _calculator;
        private readonly IUsageLogger _usageLogger;

        public Presenter(IView view) : this(view, new Calculator(), new UsageLogger())
        {
        }

        public Presenter(IView view, ICalculator calculator, IUsageLogger usageLogger)
        {
            _view = view;
            _calculator = calculator;
            _usageLogger = usageLogger;
        }

        public void Run()
        {
            if (TryParseAndLog(GetNumber(), out var numberOne))
            {
                if (TryParseAndLog(GetNumber(), out var numberTwo))
                {
                    var result = _calculator.Add(numberOne, numberTwo);
                    _usageLogger.RecordSuccess(numberOne, numberTwo, result);
                    _view.ShowResult(result.ToString());
                }
            }
        }

        private bool TryParseAndLog(string userString, out decimal asDecimal)
        {
            if (decimal.TryParse(userString, out asDecimal))
            {
                return true;
            }
            _view.Warn($"That isn't a number");
            _usageLogger.RecordError(userString);
            return false;
        }

        private string GetNumber()
        {
            var userString = _view.RequestNumber();
            if ("q".Equals(userString))
            {
                _view.Quit();
                return null;
            }

            return userString;
        }
    }
}