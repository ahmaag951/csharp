using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace using_and_dispose_overriding
{
    class Program
    {
        static void Main(string[] args)
        {
            // If the Element class wasn't implementing the IDisposable interface, you wouldn't be able to use it here.
            using (var e = new Element())
            {

                Console.WriteLine("the using is about to be finished...");
            }

            Console.Read();
        }
    }

    internal class Element : IDisposable
    {

        public void Dispose()
        {
            Console.WriteLine("The dispose method is called.");

        }
    }
}
