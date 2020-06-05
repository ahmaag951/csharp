using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace func
{
    class Program
    {
        static void Main(string[] args)
        {
            // this is a function that takes two integers and return integer
            // func makes you able to create functions that takes up to 16 parameter and last one will be the return type
            Func<int, int> f = Square;

            // if you want to write this using lambda expression 
            Func<int, int> f2 = x => x * x;

            // sum using lambda expression
            // if you have zero or more than one parameter you need parentheses
            Func<int, int, int> Sum = (x, y) => x + y;
            Func<int, int, int> SumMultipleLines = (x, y) =>
            {
                int result = x + y;
                return result;
            };

            // The Action is a pre-built delegate that does not return a value.
            Action<string> write = x => Console.WriteLine(x);
            Action<string, int> write2 = (x, y) => Console.WriteLine($"x: {x}, y: {y})");

            Action actionNonGeneric = () => Console.WriteLine("This is a non generic action that takes no parameter");
            Action actionNonGeneric2 = new Action(() =>  Console.WriteLine("This is a non generic 2 action that takes no parameter"));

            Console.WriteLine(f(4));
            Console.WriteLine(f2(4));
            Console.WriteLine(Sum(4, 3));

            write("Hello, string from Action!");
            write.Invoke("hello, from Action, by invoke!");

            write2("Hello, string from Action!", 25);

            actionNonGeneric();
            actionNonGeneric2();

            Console.Read();
        }

        private static int Square(int number)
        {
            return number * number;
        }
    }
}
