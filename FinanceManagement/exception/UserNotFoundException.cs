using System;

namespace FinanceManagementSystem.exception
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message) { }
    }
}
