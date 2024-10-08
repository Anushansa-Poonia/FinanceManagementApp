using FinanceManagementSystem.dao;
using FinanceManagementSystem.entity;
using ConsoleTables;
using System;
using System.Collections.Generic;
using FinanceManagement.exception;

namespace FinanceManagementSystem
{
    class FinanceApp
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - "Finance Management System".Length) / 2, Console.CursorTop);
            Console.WriteLine("\x1B[1m\x1B[4mFinance Management System\x1B[0m\n"); // Bold and underline heading
            Console.ResetColor();

            Console.WriteLine("Hello! Welcome to the Finance Management System.");
            Console.Write("\nLet us know your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine($"\nAre you a new user or an old user, {userName}? (Enter 'new' or 'old'):");
            string userType = Console.ReadLine().ToLower();

            try
            {
                if (userType == "new")
                {
                    SignUp(userName);  // Sign-up logic for new users, user logs in automatically after creation.
                    Console.WriteLine("User successfully logged in.");
                }
                else if (userType == "old")
                {
                    SignIn(userName);  // Sign-in logic for existing users remains unchanged.
                }
                else
                {
                    throw new InvalidUserInputException(userType);  // Handles invalid userType inputs.
                }
            }
            catch (InvalidUserInputException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }

            IFinanceRepository repository = new FinanceRepositoryImpl();
            ShowMenu(repository);

        }

        static void SignUp(string userName)
        {
            Console.WriteLine("Sign Up:");
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            User user = new User { Username = userName, Email = email, Password = password };
            IFinanceRepository repository = new FinanceRepositoryImpl();
            bool result = repository.CreateUser(user);

            if (result)
            {
                Console.WriteLine("Sign up successful! You are now logged in.");
                PrintUserTable(repository);
            }
            else
            {
                Console.WriteLine("Sign up failed. Please try again.");
                Environment.Exit(0);
            }

            PrintDashes();
        }

        static void SignIn(string userName)
        {
            Console.WriteLine("Sign In:");
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            if (email == "anushansapoonia@gmail.com" && password == "12345")
            {
                Console.WriteLine("Login successful!");
            }
            else if (email != "anushansapoonia@gmail.com" && password != "12345")
            {
                Console.WriteLine("Invalid email and password.");
                Environment.Exit(0);
            }
            else if (email != "anushansapoonia@gmail.com")
            {
                Console.WriteLine("Invalid email.");
                Environment.Exit(0);
            }
            else if (password != "12345")
            {
                Console.WriteLine("Invalid password.");
                Environment.Exit(0);
            }
        }

        static void ShowMenu(IFinanceRepository repository)
        {
            Console.WriteLine("\nWelcome to the Finance Management System!");
            int choice;
            do
            {
                Console.WriteLine("\nBelow is the menu, please type the number according to your need:");
                Console.WriteLine("1. Add User\n2. Add Expense\n3. Delete User\n4. Delete Expense\n5. Update Expense\n6. View All Expenses\n0. Exit");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddUser(repository);
                        PrintUserTable(repository); // Print updated table
                        break;
                    case 2:
                        AddExpense(repository);
                        PrintExpenseTable(repository); // Print updated table
                        break;
                    case 3:
                        DeleteUser(repository);
                        PrintUserTable(repository); // Print updated table
                        break;
                    case 4:
                        DeleteExpense(repository);
                        PrintExpenseTable(repository); // Print updated table
                        break;
                    case 5:
                        UpdateExpense(repository);
                        PrintExpenseTable(repository); // Print updated table
                        break;
                    case 6:
                        ViewAllExpenses(repository);  // Error fixed here, passing userId to the method
                        break;
                    case 0:
                        Console.WriteLine("Exiting... Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

                PrintDashes(); // Add dashes after every execution

            } while (choice != 0);
        }

        static void AddUser(IFinanceRepository repository)
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();
            User user = new User { Username = username, Password = password, Email = email };
            bool result = repository.CreateUser(user);
            Console.WriteLine(result ? "User added successfully." : "Failed to add user.");
        }

        static void AddExpense(IFinanceRepository repository)
        {
            Console.WriteLine("Enter userId:");
            int userId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter amount:");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter categoryId:");
            int categoryId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter date (yyyy-mm-dd):");
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter description:");
            string description = Console.ReadLine();
            Expense expense = new Expense { UserId = userId, Amount = amount, CategoryId = categoryId, Date = date, Description = description };
            bool result = repository.CreateExpense(expense);
            Console.WriteLine(result ? "Expense added successfully." : "Failed to add expense.");
        }

        static void DeleteUser(IFinanceRepository repository)
        {
            Console.WriteLine("Enter userId to delete:");
            int userId = Convert.ToInt32(Console.ReadLine());
            bool result = repository.DeleteUser(userId);
            Console.WriteLine(result ? "User deleted successfully." : "Failed to delete user.");
        }

        static void DeleteExpense(IFinanceRepository repository)
        {
            Console.WriteLine("Enter expenseId to delete:");
            int expenseId = Convert.ToInt32(Console.ReadLine());
            bool result = repository.DeleteExpense(expenseId);
            Console.WriteLine(result ? "Expense deleted successfully." : "Failed to delete expense.");
        }

        static void UpdateExpense(IFinanceRepository repository)
        {
            Console.WriteLine("Enter userId:");
            int userId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter expenseId:");
            int expenseId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter new amount:");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter new categoryId:");
            int categoryId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter new date (yyyy-mm-dd):");
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter new description:");
            string description = Console.ReadLine();
            Expense expense = new Expense { UserId = userId, ExpenseId = expenseId, Amount = amount, CategoryId = categoryId, Date = date, Description = description };
            bool result = repository.UpdateExpense(userId, expense);
            Console.WriteLine(result ? "Expense updated successfully." : "Failed to update expense.");
        }

        static void ViewAllExpenses(IFinanceRepository repository)
        {
            Console.WriteLine("Enter userId to view all expenses:");
            int userId = Convert.ToInt32(Console.ReadLine());  // Taking userId input here
            List<Expense> expenses = repository.GetAllExpenses(userId);  // Passing userId to method
            if (expenses.Count > 0)
            {
                foreach (var expense in expenses)
                {
                    Console.WriteLine($"Expense ID: {expense.ExpenseId}, Amount: {expense.Amount}, Category ID: {expense.CategoryId}, Date: {expense.Date}, Description: {expense.Description}");
                }
            }
            else
            {
                Console.WriteLine("No expenses found for this user.");
            }
        }

        // Method to print dashes for cleaner output
        static void PrintDashes()
        {
            Console.WriteLine(new string('-', 40));
        }

        // Method to print the updated user table in pink using ConsoleTables
        static void PrintUserTable(IFinanceRepository repository)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            List<User> users = repository.GetAllUsers();
            var table = new ConsoleTable("UserId", "Username", "Email");
            foreach (var user in users)
            {
                table.AddRow(user.UserId, user.Username, user.Email);
            }
            table.Write();
            Console.ResetColor();
        }

        // Method to print the updated expense table in pink using ConsoleTables
        static void PrintExpenseTable(IFinanceRepository repository)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            List<Expense> expenses = repository.GetAllExpenses(0);  // Assuming 0 can be passed for getting all expenses
            var table = new ConsoleTable("ExpenseId", "UserId", "Amount", "CategoryId", "Date", "Description");
            foreach (var expense in expenses)
            {
                table.AddRow(expense.ExpenseId, expense.UserId, expense.Amount, expense.CategoryId, expense.Date.ToString("yyyy-MM-dd"), expense.Description);
            }
            table.Write();
            Console.ResetColor();
        }
    }
}
