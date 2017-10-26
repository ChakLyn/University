using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordGenerator.Utils;

namespace PasswordGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter strength for password: ");
            int strength = Int32.Parse(Console.ReadLine());
            Console.Write("Enter length for password: ");
            int length = Int32.Parse(Console.ReadLine());
            Console.Write("Enter count of passwords: ");
            int count = Int32.Parse(Console.ReadLine());
            Console.WriteLine(Generator.GeneratePassword(strength,length,count));
            Console.ReadLine();
        }
    }
}
