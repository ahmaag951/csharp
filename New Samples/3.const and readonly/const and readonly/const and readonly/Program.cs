using System.Diagnostics.Metrics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace const_and_readonly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
             /*           
                                 | `const`                                 | `readonly`                |
                    | ---------------------------------------   | ----------------------------------   |
                    | Value must be known at compile time       | Value can be set at runtime          |
                    | Must be assigned when declared            | Can be assigned in constructor       |
                    | Implicitly  static                        | Can be instance or static            |
                    | Never changes                             | Cannot change after construction     |
             */

            // The const is Implicitly static (belongs to the class itself, not to each object)
            // so all the circles share the same value of pi
            Console.WriteLine("Pi: " + Circle.pi);

            // readonly Can be instance or static, in this case it's static
            Console.WriteLine("Readonly static variable: " + Circle.readonlyCompileTimeVariable);

            // and in this case, it's instance readonly, so each object can have its own value.
            Circle c = new Circle(5);
            Console.WriteLine("Area of circle with radius " + c.radius + " is " + c.Area()); // runtime constant
        }

    }

    public class Circle
    {
        public const double pi = 3.14; // compile time constant
        // we use the static readonly when the value is shared by all objects and the value should be set once only
        // for example Example: App version, Database connection string, Config values loaded at startup

        public static readonly int readonlyCompileTimeVariable = 5; // compile time constant

        public readonly double radius; // runtime constant
        public Circle(double radius)
        {
            this.radius = radius; // can be assigned in constructor
        }

        public double Area()
        {
            //radius = 5; // you cannot change readonly value after construction
            return pi * radius * radius; 
        }
    }
}
