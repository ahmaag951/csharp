using System;

namespace test_extension_method
{
    class Program
    {
        static void Main(string[] args)
        {
            // Demonstrate the extension method
            string original = "qwer";
            string result = original.AddABC();
            
            Console.WriteLine("Original string: " + original);
            Console.WriteLine("After AddABC(): " + result);
            
            Console.ReadKey();
        }
    }
}
