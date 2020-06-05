using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace three_cases_you_can_override_method_in
{
    class Program
    {
        static void Main(string[] args)
        {
            var myClass = new MyClass();
            Console.WriteLine(myClass.Display());
            var subClass = new SubClass();
            Console.WriteLine(subClass.Display());
            var subsubClass = new SubSubClass();
            Console.WriteLine(subsubClass.Display());
            Console.Read();
        }
    }


    class MyClass
    {
        public int Id { get; set; }

        public virtual string Display()
        {
            return "Hello from my class";
        }
    }

    class SubClass : MyClass
    {
        // 1. I could override this because the parent is virtual
        public override string Display()
        {
            return "Hello from sub class";
        }
    }

    class SubSubClass : SubClass
    {
        // 2. I could override this because the parent is override
        public override string Display()
        {
            return "Hello from sub sub class";
        }
    }
}
