using System;
using System.Xml.Schema;

namespace MathApp
{
    public interface IView
    {
        void Warn(string message);
        IUserInput RequestNumber();
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
            var numberOne = _view.RequestNumber();
            if (BadInputOrQuit(numberOne))
            {
                return;
            }
            var numberTwo = _view.RequestNumber();
            if (BadInputOrQuit(numberTwo))
            {
                return;
            }

            var addend1 = numberOne.Number;
            var addend2 = numberTwo.Number;
            var result = _calculator.Add(addend1, addend2);
            _usageLogger.RecordSuccess(addend1, addend2, result);
            _view.ShowResult(result.ToString());
        }

        private bool BadInputOrQuit(IUserInput numberOne)
        {
            if (numberOne.IsQuit)
            {
                _view.Quit();
                return true;
            }

            if (numberOne.IsBad)
            {
                _view.Warn($"That isn't a number");
                _usageLogger.RecordError(numberOne.RawValue);
                return true;
            }
            return false;
        }
    }
}