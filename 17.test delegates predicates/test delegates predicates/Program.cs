using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_delegates_predicates
{
    class Program
    {
        static void Main(string[] args)
        {
            // A predicate is a function that returns true or false. 
            // A predicate delegate is a reference to a predicate.

            Predicate<string> IsUpperCasePredicate = IsUpperCase; 

            bool result1 = IsUpperCasePredicate("Hello");
            bool result2 = IsUpperCasePredicate("HELLO");

            Console.WriteLine(result1 + "\n" + result2);

            // the predicate is a delegate that returns bool
            // Predicate<T> is basically identical to Func<T,bool>, and will always return a boolean

            Predicate<string> predicateStartsWithA = s => s.StartsWith("A");
            Predicate<string> predicateUsingDelegateStartsWithA = delegate (string s) { return s.StartsWith("A"); };

            Console.WriteLine(predicateStartsWithA("Tea")); // returns false
            Console.WriteLine(predicateStartsWithA("Apple")); // returns true
            Console.WriteLine(predicateUsingDelegateStartsWithA("Apple")); // returns true

            // another syntax for delegate is to put delegate keyword before the parameters of the method 
            // like this for example, I just coppied the method and put delegate keyword before it
            Predicate<string> testDelegate = delegate (string str)
            {
                return str.Equals(str.ToUpper());
            };
            Console.Read();
        }

        public static bool IsUpperCase(string str)
        {
            return str.Equals(str.ToUpper());
        }

        

    }
}
