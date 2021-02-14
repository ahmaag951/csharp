using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concrete_class_with_virtual_method
{
    class Program
    {
        static void Main(string[] args)
        {
            var normalConcreteClass = new NormalConcreteClass();
            var childClass = new ChildClass();
            Console.WriteLine(normalConcreteClass.GetId());
            Console.WriteLine(childClass.GetId());
            // Please note that the virtual keyword doesn't do anything, except in the child class
            Console.Read();
        }
    }

    internal class NormalConcreteClass
    {
        public string Name { get; set; }

        public virtual int GetId()
        {
            return 5;
        }
    }

    internal class ChildClass : NormalConcreteClass
    {
        // Without the virtual keyword you can't override the GetId method here
        public override int GetId()
        {
            return 23;
        }
    }
}
