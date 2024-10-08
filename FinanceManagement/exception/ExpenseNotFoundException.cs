using System;

namespace FinanceManagementSystem.exception
{
    public class ExpenseNotFoundException : Exception
    {
        public ExpenseNotFoundException(string message) : base(message) { }
    }
}
