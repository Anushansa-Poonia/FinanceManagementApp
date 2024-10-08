namespace FinanceManagementSystem.entity
{
    public class User
    {
        public int UserId { get; set; } // Keep UserId
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        // Remove Id to avoid confusion
        public User() { }

        public User(int userId, string username, string password, string email)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Email = email;
        }
    }
}
