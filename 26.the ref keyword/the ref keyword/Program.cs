using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_ref_keyword
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 5;
            // This will not change the value of x
            PlusTen(x);
            Console.WriteLine(x);

            // This will change the value of x
            PlusTenWithRef(ref x);
            Console.WriteLine(x);

            Console.Read();
        }

        private static int PlusTen(int x)
        {
            x += 10;
            return x;
        }
        private static int PlusTenWithRef(ref int x)
        {            
            x += 10;
            return x;
        }
    }
}
