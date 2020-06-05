using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegate_with_square_and_cube_methods
{
    class Program
    {
        // Define the delegate
        private delegate int NumberFunction(int x);

        static void Main(string[] args)
        {
            int x = 2;
            NumberFunction f = Square;
            Console.WriteLine(f(x));

            // now you are changing the delegate on the fly
            // while the program is running you are changing what the delegate (the method) is doing
            f = Cube;
            Console.WriteLine(f(x));

            Console.Read();
        }

        private static int Square(int x)
        {
            return x * x;
        }

        private static int Cube(int x)
        {
            return x * x * x;
        }
    }
}
