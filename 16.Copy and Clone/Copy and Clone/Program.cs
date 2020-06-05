using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copy_and_Clone
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new int[] { 2, 3, 8, 9 };
            var arrayCopy = array; // they refrence the same thing

            arrayCopy[0] = 0;
            Console.WriteLine(array[0]); // will print 0 also.

            var arrayCopyTo = new int[array.Length];
            array.CopyTo(arrayCopyTo, 0); // they are two diffrenct arrays

            array[2] = 5;
            Console.WriteLine(arrayCopyTo[2]);

            var arrayClone = (int[])array.Clone(); // creates a shallow copy of the array, the two arrays are seperated.

            arrayClone[1] = 0;
            Console.WriteLine(array[1]); // will not print 0 also.

            // clone - create something new based on something that exists.
            // copying - copy from something that exists to something else (that also already exists).

            Console.ReadKey();
        }
    }
}
