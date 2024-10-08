using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FinanceManagementSystem.util
{
    public class DBPropertyUtil
    {
        public static string GetPropertyString(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"The file '{fileName}' was not found.");
            }

            var jsonData = File.ReadAllText(fileName);
            var jsonObject = JObject.Parse(jsonData);

            // Null check for the "ConnectionString" key
            if (jsonObject["ConnectionString"] == null)
            {
                throw new Exception("ConnectionString not found in the configuration file.");
            }

            string connectionString = jsonObject["ConnectionString"]?.ToString();
            return connectionString ?? throw new Exception("Connection string is null or empty.");
        }
    }
}
