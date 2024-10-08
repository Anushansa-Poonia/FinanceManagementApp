using FinanceManagementSystem.entity;
using FinanceManagementSystem.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FinanceManagementSystem.dao
{
    public class FinanceRepositoryImpl : IFinanceRepository
    {
        private static SqlConnection connection;

        public FinanceRepositoryImpl()
        {
            connection = DBConnUtil.GetConnection();
        }

        public bool CreateUser(User user)
        {
            using (var conn = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Users (username, password, email) VALUES (@username, @password, @email)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@email", user.Email);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool CreateExpense(Expense expense)
        {
            using (var conn = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Expenses (user_id, amount, category_id, date, description) VALUES (@user_id, @amount, @category_id, @date, @description)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user_id", expense.UserId);
                cmd.Parameters.AddWithValue("@amount", expense.Amount);
                cmd.Parameters.AddWithValue("@category_id", expense.CategoryId);
                cmd.Parameters.AddWithValue("@date", expense.Date);
                cmd.Parameters.AddWithValue("@description", expense.Description);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteUser(int userId)
        {
            using (var conn = DBConnUtil.GetConnection())
            {
                // First, delete all expenses associated with the user
                string deleteExpensesQuery = "DELETE FROM Expenses WHERE user_id = @userId";
                SqlCommand deleteExpensesCmd = new SqlCommand(deleteExpensesQuery, conn);
                deleteExpensesCmd.Parameters.AddWithValue("@userId", userId);
                deleteExpensesCmd.ExecuteNonQuery();

                // Now, delete the user
                string deleteUserQuery = "DELETE FROM Users WHERE user_id = @userId";
                SqlCommand deleteUserCmd = new SqlCommand(deleteUserQuery, conn);
                deleteUserCmd.Parameters.AddWithValue("@userId", userId);
                return deleteUserCmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteExpense(int expenseId)
        {
            using (var conn = DBConnUtil.GetConnection())
            {
                string query = "DELETE FROM Expenses WHERE expense_id = @expenseId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@expenseId", expenseId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<Expense> GetAllExpenses(int userId)
        {
            List<Expense> expenses = new List<Expense>();
            using (var conn = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Expenses WHERE user_id = @userId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        expenses.Add(new Expense
                        {
                            ExpenseId = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            Amount = reader.GetDecimal(2),
                            CategoryId = reader.GetInt32(3),
                            Date = reader.GetDateTime(4),
                            Description = reader.GetString(5)
                        });
                    }
                }
            }
            return expenses;
        }

        public bool UpdateExpense(int userId, Expense expense)
        {
            using (var conn = DBConnUtil.GetConnection())
            {
                string query = "UPDATE Expenses SET amount = @amount, category_id = @category_id, date = @date, description = @description WHERE user_id = @userId AND expense_id = @expenseId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@expenseId", expense.ExpenseId);
                cmd.Parameters.AddWithValue("@amount", expense.Amount);
                cmd.Parameters.AddWithValue("@category_id", expense.CategoryId);
                cmd.Parameters.AddWithValue("@date", expense.Date);
                cmd.Parameters.AddWithValue("@description", expense.Description);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (var conn = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Users";
                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserId = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                            Email = reader.GetString(3)
                        });
                    }
                }
            }
            return users;
        }
    }
}
