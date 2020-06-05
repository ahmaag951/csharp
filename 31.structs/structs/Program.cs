using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace structs
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    // If you remove the keyword class and put struct no errors will happen,
    // Because they are identically the same, and do the same job
    // But struct is used when you have less functionality thing
    // And you don't need advanced features like
    // Inheritance, refrence type
    // Please note that in structs you can't initialize fields
    internal class Point
    {
        public int XCordinate { get; set; }
        public int YCordinate { get; set; }

        // Only this like will make error when you change the class to be struct.
        private int number = 5;

        public void GetMultiply()
        {
            // do anything
        }
    }


}
