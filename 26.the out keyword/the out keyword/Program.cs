using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_out_keyword
{
    class Program
    {
        //- out allows a return value to be passed back via a parameter
        //- also you can use out if you want a mehtod to return more than one value

        static void Main(string[] args)
        {
            double x = 9.0, theSquare, theRoot;
            // You have to make them out
            sqrAndRoot(x, out theSquare, out theRoot);
            Console.WriteLine("The number is: " + x + "\nThe square is: " + theSquare + "\nand the sqare root is: " + theRoot);

            Console.Read();
        }

        // This method take a number and return its sqr and its root using the out        
        private static void sqrAndRoot(double number, out double sqr, out double root)
        {
            // You have to assign values to the out parameters (sqr, root)
            root = Math.Sqrt(number);
            sqr = number * number;
        }
    }
}
