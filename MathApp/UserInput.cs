using System;

namespace MathApp
{
    public interface IUserInput
    {
        bool IsBad { get; }
        bool IsQuit { get; }
        bool IsNumber { get; }
        decimal Number { get; }
        string RawValue { get; }
    }

    public class UserInput : IUserInput
    {
        public UserInput(string input)
        {
            RawValue = input;
            if ("q".Equals(input))
            {
                IsQuit = true;
            }

            IsNumber = decimal.TryParse(input, out var good);
            if (IsNumber)
            {
                Number = good;
            }
            else
            {
                IsBad = true;
            }
        }

        public bool IsBad { get; }
        public bool IsQuit { get; }
        public bool IsNumber { get; }
        public decimal Number { get; }
        public string RawValue { get; }
    }
}