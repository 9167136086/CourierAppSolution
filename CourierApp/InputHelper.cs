using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp
{
    public static class InputHelper
    {
        public static string ReadString(string message)
        {
            while (true)
            {
                Console.Write(message);
                var input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.WriteLine("Value cannot be empty. Please try again.");
            }
        }

        public static double ReadDouble(string message)
        {
            while (true)
            {
                Console.Write(message);

                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;

                Console.WriteLine("Invalid input. Please enter a numeric value.");
            }
        }
    }
}
