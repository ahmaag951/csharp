using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace operator_overloading_minus_string
{
    class Program
    {
        static void Main(string[] args)
        {
            // This line makes error by default and say: the minus operator can't be applied to two strings
            // string test = "AhmadEzzat" - "Ezzat";
            // You can't make operator overloading with string because you can't write in it, or inherit from it
            MyClass one = new MyClass() { Name = "Ahmad" };
            MyClass two = new MyClass() { Name = "Mohd" };

            // If you were not overloading the - operator in the class, this line will make a compile error
            Console.WriteLine(one - two );
            Console.Read();
        }

    }

    class MyClass {
        public string Name { get; set; }
        public static string operator -(MyClass first, MyClass second)
        {
            return first.Name + " - " + second.Name;
        }
    }
}
