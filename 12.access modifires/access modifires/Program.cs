using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace access_modifires
{
    class Program
    {
        static void Main(string[] args)
        {
            var myClass = new MyClass();
            // You can only see the public, and you can't see the private
            // Also you can't see the protected because you are not a child of the class (you didn't inherit from it)
            myClass.PublicInt = 3;
            // You can also see the internal variables
            myClass.Internalnt = 7;
        }
    }


    class MyClass
    {
        // By default any variables in the class are private.
        // and it's only seen within the class itself
        int PrivateInt = 5;
        public int PublicInt = 4;
        protected int ProtectedInt = 3;
        internal int Internalnt = 6;

    }

    // The class by default is internal
    // Which means you can use it on the same assembly (package or solution or library) only
    // So in the project that called Another Project you wont be able to see it
    // But you will be able to see the public class
    class InternalClass
    {

    }

    public class PublicClass
    {
        internal int Internalnt = 3;

    }

    // You can't create a private class!!! unless it's inside another class
    //private class PrivateClass
    //{

    //}

    class ChildClass : MyClass
    {
        // you can create private class in another class
        private class PrivateClass
        {

        }

    }
}
