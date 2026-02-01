using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jagged_array
{
    class Program
    {
        static void Main(string[] args)
        {
            // jagged array is an array of arrays
            // it's a 1 dimenional array of arrays
            int[][] jaggedArray = new int[3][];

            jaggedArray[0] = new int[] { 1, 2, 3 };
            jaggedArray[1] = new int[] { 1, 2, 3, 4 };
            jaggedArray[2] = new int[2];

            foreach (var item in jaggedArray)
            {
                foreach (var i in item)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine("-----");
            }

            Console.WriteLine("-------------------------------------------");

            int[,] twoDimentionalArray = new int[3, 2];

            twoDimentionalArray[0, 0] = 4;

            foreach (var item in twoDimentionalArray)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
