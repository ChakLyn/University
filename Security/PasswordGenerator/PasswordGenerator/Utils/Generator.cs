using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGenerator.Utils
{
    public static class Generator
    {
        private static List<char> _range = new List<char>();

        private static Random random = new Random();

        static Generator()
        {
            _range.AddRange(Enumerable.Range('A', 26).Select(item => (char)item));
            _range.AddRange(Enumerable.Range('a', 26).Select(item => (char)item));
            _range.AddRange(Enumerable.Range('0', 10).Select(item => (char)item));
            _range.AddRange(Enumerable.Range('!', 7).Select(item => (char)item));
            _range.AddRange(Enumerable.Range('А', 32).Select(item => (char)item));
            _range.AddRange(Enumerable.Range('а', 32).Select(item => (char)item));
        }

        public static string GeneratePassword(int strength, int length, int count)
        {
            StringBuilder password = new StringBuilder();

            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < length; i++)
                {
                    password.Append(_range[random.Next(0, strength)]);
                }
                password.AppendLine();
            }
            return password.ToString();
        }
    }
}
