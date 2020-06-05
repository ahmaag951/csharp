using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace type_of
{
    class Program
    {
        static void Main(string[] args)
        {
            // type of returns a string represents the type of object (the class name)
            var type = typeof(Student);
            var type2 = typeof(int);

            Console.WriteLine(type);
            Console.WriteLine(type2);

            Student s = new Student();
            
            // Type of work only when you give it a class not object
            //type = typeof(s);
            Console.Read();
        }
    }

    class Student
    {
         
    }
}
