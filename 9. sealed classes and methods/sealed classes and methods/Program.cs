using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sealed_classes_and_methods
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseClass = new BaseClass();
            Console.WriteLine(baseClass.Display());
            var subClass = new SubClass();
            Console.WriteLine(subClass.Display());

            Console.Read();
        }
    }

    // If you make this class sealed class, a compile error will happen
    class BaseClass
    {
        public int Id { get; set; }

        public string Display()
        {
            return "Hello from base class";
        }

        // we make this virtual so that we can override it in the sub class
        public virtual void TestSealedOverride()
        {
        }
    }

    class SubClass : BaseClass
    {
        public string Display()
        {
            return "Hello from sub class";
        }

        // you can only make the overrid methods sealed, but you can't with the normal methods
        // now you can create sealed method, because it's an override.
        public sealed override void TestSealedOverride()
        {
            Console.WriteLine("I'm the TestSealedOverride");
        }
    }

    class TestSealedMethod
    {
        // if you tried to make a normal method sealed, you will get an error says: 
        // Method can't be sealed because it's not an override.
        //public sealed void test()
        //{
        //}
    }

    class SubSubClass : SubClass
    {
        // you can't override this, because it's a sealed method.
        //public override void TestSealedOverride()
        //{

        //}
    }
}
