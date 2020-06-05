using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace try_parse
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "7788";
            // instead of defining the variable in seperate line
            //int num1;
            bool n = int.TryParse(str, out int num1);
            Console.WriteLine(num1);
            Console.WriteLine(n);

            Console.Read();
        }
    }
}
