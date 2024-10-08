using FinanceManagementSystem.entity;
using System.Collections.Generic;

namespace FinanceManagementSystem.dao
{
    public interface IFinanceRepository
    {
        bool CreateUser(User user); // Make sure this is defined correctly
        bool CreateExpense(Expense expense); // Ensure Expense is defined
        bool DeleteUser(int userId);
        bool DeleteExpense(int expenseId);
        List<Expense> GetAllExpenses(int userId);  // Requires userId
        bool UpdateExpense(int userId, Expense expense);
        List<User> GetAllUsers(); // Ensure this method is included
    }
}
