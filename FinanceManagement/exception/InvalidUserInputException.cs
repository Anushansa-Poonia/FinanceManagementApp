using System;

namespace FinanceManagement.exception
{
    public class InvalidUserInputException : Exception
    {
        public InvalidUserInputException(string input)
            : base($"Invalid input provided: '{input}'. Please enter a valid value.")
        {
        }
    }
}
