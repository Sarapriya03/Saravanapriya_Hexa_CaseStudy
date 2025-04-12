using System;
using System.IO;
using System.Collections.Generic;

namespace PayXpert.util
{
    public class DBPropertyUtil
    {
        public static string GetConnectionString(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

            // Log paths for debugging
            Console.WriteLine($"Input FileName: {fileName}");
            Console.WriteLine($"Current Directory: {Environment.CurrentDirectory}");

            // 🔧 Hardcoded absolute path for debugging (remove after confirming)
            string fullPath = @"E:\\Hexaware\\Case Study\\Saravanapriya_Hexa_CaseStudy\\PayXpert\\PayXpert\\db.properties";
            Console.WriteLine("Trying to read from: " + fullPath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"The file '{fullPath}' does not exist.");

            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            try
            {
                var lines = File.ReadAllLines(fullPath);

                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                    {
                        var parts = line.Split('=', 2);
                        dict[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error reading the properties file.", ex);
            }

            // Required keys
            if (!dict.ContainsKey("Server") || !dict.ContainsKey("Database"))
                throw new KeyNotFoundException("The properties file must contain 'Server' and 'Database' keys.");

            // ✅ Windows Authentication - Use Trusted_Connection
            return $"Server={dict["Server"]};Database={dict["Database"]};Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;";

        }
    }
}
