using System;
using System.Data.SqlClient;
using FinanceManagementSystem.exception;

namespace FinanceManagementSystem.util
{
    public class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            try
            {
                // Ensure "appsettings.json" is in the correct location
                string connectionString = DBPropertyUtil.GetPropertyString("appsettings.json");

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new Exception("Connection string is empty or null.");
                }

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {
                // Log the actual exception for debugging purposes
                Console.WriteLine($"SQL Exception: {ex.Message}");
                throw new Exception("An error occurred while establishing a database connection.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
    }
}
