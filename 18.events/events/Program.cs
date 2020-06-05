using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace events
{
    public delegate void myEventHandler(string newValue);
    class Program
    {
        // If you have a class and you want to let all the class's users to be acknoledged when a value changed
        static void Main(string[] args)
        {
            MyClass myClass = new MyClass();
            myClass.valueChanged += new myEventHandler(myClass_ValueChanged);

            myClass.Val = "Joe is here";

            Console.Read();
        }

        private static void myClass_ValueChanged(string newValue)
        {
            Console.WriteLine("The value change to: " + newValue);
        }
    }

    class MyClass
    {
        private string theValue;
        public event myEventHandler valueChanged;

        public string Val
        {
            set
            {
                theValue = value;
                this.valueChanged(value);
            }
        }

    }
}
