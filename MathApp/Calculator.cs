namespace MathApp
{
    public interface ICalculator
    {
        decimal Add(decimal addend1, decimal addend2);
    }

    public class Calculator : ICalculator
    {
        public decimal Add(decimal addend1, decimal addend2)
        {
            return addend1 + addend2;
        }
    }
}