using FinanceManagementSystem.entity;
using FinanceManagementSystem.dao;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using FinanceManagementSystem.exception;

namespace FinanceManagement.nUnitTests
{
    public class MockFinanceRepository : IFinanceRepository
    {
        private readonly List<User> _users = new List<User>();
        private readonly List<Expense> _expenses = new List<Expense>();

        public bool CreateUser(User user)
        {
            _users.Add(user);
            return true;
        }

        public bool CreateExpense(Expense expense)
        {
            _expenses.Add(expense);
            return true;
        }

        public bool DeleteUser(int userId)
        {
            var user = _users.Find(u => u.UserId == userId);
            if (user == null)
            {
                throw new UserNotFoundException($"User with ID {userId} not found.");
            }
            _users.Remove(user);
            return true;
        }

        public bool DeleteExpense(int expenseId) => true;

        public List<Expense> GetAllExpenses(int userId) => _expenses;

        public bool UpdateExpense(int userId, Expense expense) => true;

        public List<User> GetAllUsers() => _users;
    }

    public class UserCreationTests
    {
        private IFinanceRepository _financeRepository;

        [SetUp]
        public void Setup()
        {
            _financeRepository = new MockFinanceRepository();
        }

        [Test]
        public void Test_CreateUser_Success()
        {
            var uniqueUsername = $"testuser_{Guid.NewGuid()}";
            var uniqueEmail = $"testuser{Guid.NewGuid()}@example.com";

            var user = new User
            {
                Username = uniqueUsername,
                Password = "testpassword",
                Email = uniqueEmail
            };

            // Act
            bool result = _financeRepository.CreateUser(user);

            // Assert
            Assert.That(result, Is.True, "User should be created successfully.");
        }

        [Test]
        public void Test_CreateExpense_Success()
        {
            // Arrange
            var expense = new Expense
            {
                UserId = 1,
                Amount = 150.00m,
                CategoryId = 1,
                Date = DateTime.Now,
                Description = "Test expense",
                ExpenseId = 1 // Assigning a sample ExpenseId
            };

            // Act
            bool result = _financeRepository.CreateExpense(expense);

            // Assert
            Assert.That(result, Is.True, "Expense should be created successfully.");
        }

        [Test]
        public void Test_GetExpenseById_Success()
        {
            // Arrange
            var expense = new Expense
            {
                UserId = 1,
                Amount = 150.00m,
                CategoryId = 1,
                Date = DateTime.Now,
                Description = "Test expense",
                ExpenseId = 1 // Assigning a sample ExpenseId
            };
            _financeRepository.CreateExpense(expense); // Make sure the expense is created

            int expenseId = 1;

            // Act
            var foundExpense = _financeRepository.GetAllExpenses(1).Find(e => e.ExpenseId == expenseId);

            // Assert
            Assert.That(foundExpense, Is.Not.Null, "Expense should be found.");
            Assert.That(foundExpense.ExpenseId, Is.EqualTo(expenseId), "The found expense ID should match the requested ID.");
        }

        [Test]
        public void Test_DeleteUser_Exception()
        {
            int invalidUserId = 999; // Assume this user ID does not exist in the repository

            // Act & Assert
            var ex = Assert.Throws<UserNotFoundException>(() => _financeRepository.DeleteUser(invalidUserId));
            Assert.That(ex.Message, Is.EqualTo($"User with ID {invalidUserId} not found."));
        }
    }
}
