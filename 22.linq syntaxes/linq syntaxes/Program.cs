using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq_syntaxes
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            // 1. "Query Syntax" is like the sql syntax
            var querySyntax = from query
                              in list
                              where query >= 5
                              select query;

            foreach (var item in querySyntax)
            {
                Console.Write(item);
            }
            Console.WriteLine();

            // 2. "Method Syntax"
            var methodSyntax = list.Where(r => r >= 5);

            foreach (var item in methodSyntax)
            {
                Console.Write(item);
            }

            Console.Read();
        }
    }
}
