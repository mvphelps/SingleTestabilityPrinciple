using MathApp;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class PresenterTests
    {
        private Mock<IView> _view;
        private Mock<ICalculator> _calculator;
        private Presenter _presenter;
        private Mock<IUsageLogger> _usageLogger;

        [SetUp]
        public void SetUp()
        {
            _view = new Mock<IView>();
            _usageLogger = new Mock<IUsageLogger>();
            _calculator = new Mock<ICalculator>();
            _presenter = new Presenter(_view.Object, _calculator.Object,  _usageLogger.Object);
        }

        [Test]
        public void WarnsWhenFirstNotNumber()
        {
            _view.Setup(x => x.RequestNumber()).Returns("a");

            _presenter.Run();

            _view.Verify(x => x.Warn("That isn't a number"));
        }

        [Test]
        public void LogsErrorWhenNotNumber()
        {
            _view.Setup(x => x.RequestNumber()).Returns("a");

            _presenter.Run();

            _usageLogger.Verify(x => x.RecordError("a"));
        }

        [Test]
        public void WarnsWhenSecondIsNotNumber()
        {
            _view.SetupSequence(x => x.RequestNumber()).Returns("1").Returns("b");

            _presenter.Run();

            _view.Verify(x => x.Warn("That isn't a number"));
        }

        [Test]
        public void AddsAndLogsWhenBothAreNumbers()
        {
            _view.SetupSequence(x => x.RequestNumber()).Returns("1").Returns("38");
            _calculator.Setup(x => x.Add(1, 38)).Returns(6);

            _presenter.Run();

            _view.Verify(x => x.ShowResult("6"));
            _usageLogger.Verify(x => x.RecordSuccess(1, 38, 6));
        }

        [Test]
        public void QuitsWhenUserEntersQ()
        {
            _view.Setup(x => x.RequestNumber()).Returns("q");

            _presenter.Run();

            _view.Verify(x => x.Quit());
        }
    }
}