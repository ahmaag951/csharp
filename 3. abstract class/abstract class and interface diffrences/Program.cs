using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abstract_class_and_interface_diffrences
{
    class Program
    {
        static void Main(string[] args)
        {
            // You can't create instance from an abstract class
            // The abstract class is created in the first place to be inherit from
            // AbstractClass abstractClass = new AbstractClass();
            var childClass = new ChildClass();
            childClass.Display();
            childClass.AbstractMethod();

            Console.Read();
        }
    }

    abstract class AbstractClass
    {
        public void Display()
        {
            Console.WriteLine("disply from abstract class");
        }

        // The abstract method is a method header without body
        // And the children of the abstract class has to implement the body
        public abstract void AbstractMethod();

        // you can't create abstract field
        //public abstract int x;
    }

    class ChildClass : AbstractClass
    {
        // I had to override this method, because it's an abstract method
        // The combiler forced me to do this
        public override void AbstractMethod()
        {
            Console.WriteLine("disply from abstract method in child class");            
        }
    }
}
