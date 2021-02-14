using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refrence_type
{
    class Program
    {
        static void Main(string[] args)
        {

            Student s1 = new Student() { Name = "Ahmad" };
            Student s2 = s1; // Now they are pointing to the same place in the memory, also s1.name, s2.name is the same in the memory

            Console.WriteLine(s2.Name);
            s1.Name = "Mohd";

            Console.WriteLine(s2.Name); // will print Mohd

            Console.Read();
        }
    }

    internal class Student
    {
        public string Name { get; set; }
    }
}
