using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using access_modifires;

namespace Another_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            // Because this is a seperate project, you can only see the public class
            // And you can't see the internal class
            PublicClass publicClass = new PublicClass();
            // You can't see the internal varibales in it.

        }
    }
}
