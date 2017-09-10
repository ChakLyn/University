using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AILab_1.Utils.OutputHelper
{
    public static class TypingHelper
    {
        public static void TypeString(string inputString)
        {
            foreach (char charecter in inputString)
            {
                Console.Write(charecter);
                Thread.Sleep(150);
            }
            Console.WriteLine();
        }
    }
}
