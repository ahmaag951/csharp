using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get all types (classes) used in the current assembly
            Type[] typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in typesInThisAssembly)
            {
                Console.WriteLine(type.Name);
            }

            Console.WriteLine("------------");
            // you can also use reflection to get info from an assembly
            Assembly info = typeof (System.Int32).Assembly;
            Console.WriteLine(info);

            Console.ReadKey();
        }
    }
}
