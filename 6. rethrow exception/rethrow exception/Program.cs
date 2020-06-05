using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rethrow_exception
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                doSomeMath();
            }
            catch (ArithmeticException e)
            {
                Console.WriteLine("This from the ArithmeticException");
            }

            finally
            {
                Console.Read();
            }
        }

        private static void doSomeMath()
        {
            try
            {
                int x = 0;
                double y = 10 / x;
            }
            catch (DivideByZeroException e)
            {
                // If you tried to handle and solve the problem but you couldn't
                // you can rethrow the exception like this
                // 1. by throw; you are throwing the current exception with all its type
                //throw; // this will not make any problems because the ArithmeticException is a base class 
                // for dividebyzro so it will be handled also by the arithmatic even though it's a dividebyzero
                // or you can throw new exception
                Console.WriteLine("This from the DivideByZeroException");
                throw new ArithmeticException();
            }

        }
    }
}
