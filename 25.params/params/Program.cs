using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace @params
{
    class Program
    {
        static void Main(string[] args)
        {
            // You can't use params int[] numbers here
            // You have to make it in a method
            var result = AddNumbers(1, 2);
            Console.WriteLine(result);
            result = AddNumbers(1, 2, 6);
            Console.WriteLine(result);
            // You can also do the same with list
            // But the params call is simply faster because it is a bit faster to create an array than it is to create a List<>
            result = AddNumbersWithList(new List<int>(){1, 2});
            Console.WriteLine(result);

            Console.Read();
        }

        // Please note that if you want to send another variable in the method signature
        // The params list has to be the last argument
        // For example you can't say AddNumbers(params int[] numbers, string s)
        // AddNumbers(string s, params int[] numbers) won't make errors
        // The reason is the combiler need to know the number of parameters before starting to compile the params list
        private static int AddNumbers(params int[] numbers)
        {
            var sum = numbers.Sum();
            return sum;
        }

        private static int AddNumbersWithList(List<int> numbers)
        {
            var sum = numbers.Sum();
            return sum;
        }
    }
}
