using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ref_and_out
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             The two of them are used to let the function change the value of the passed parameters
             ref tells the compiler that the object is initialized before entering the function, 
             * while out tells the compiler that the object will be initialized inside the function.
             */
            var x = 3;
            PlusOneOut(out x);
            // this is another easier syntax
            PlusOneOut(out int a);
            PlusOneRef(ref x);
            Console.WriteLine(a);
            Console.WriteLine(x);
            Console.Read();
        }

        public static void PlusOneOut(out int x)
        {
            // This initialization is important
            x = 3;
            x += 1;
            
        }

        public static void PlusOneRef(ref int x)
        {
            x += 1;

        }
    }

    
}
