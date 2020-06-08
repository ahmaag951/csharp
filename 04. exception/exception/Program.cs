using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exception
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int x = 0;
                double y = 10 / x;
            }
            // Please note that the DivideByZeroException is a subclass from ArithmeticException
            // And the nearst one to the exception is the one that is going to handle it
            // So what is going to happend is that the DivideByZeroException is going to catch the exception
            // And the ArithmeticException will not do anything
            catch (DivideByZeroException e)
            {
                Console.WriteLine("divide by zero exception");
                Console.WriteLine("Exception msg: " + e.Message);
            }
            // Please note that if you put the ArithmeticException before the DivideByZeroException 
            // a compliation error will happen an say "A previous catch clause already catches all exceptions of this 
            // or of a super type
            catch (ArithmeticException e)
            {
                Console.WriteLine("arithmatic exception");
                Console.WriteLine(e.Message);
            }
            finally
            {
                // This code will be executed wheather an error happend or not
                Console.Read();
            }
        }
    }
}
