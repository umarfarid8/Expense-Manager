using System.Security.Cryptography;
using System.Text;

namespace Expense_Manager.Utilities
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                    
                }
                return builder.ToString();
        }
        
            public static bool VerifyPassword(string inputPassword, string storedhash)
            {
            string hashOfInput = HashPassword(inputPassword);
            
                return string.Equals(hashOfInput, storedhash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
