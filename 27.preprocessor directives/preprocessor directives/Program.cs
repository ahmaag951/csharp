// Please note that this should be the first thing in the file
#define DEBUGMOOD
//#define JOE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace preprocessor_directives
{
    class Program
    {
        static void Main(string[] args)
        {
            // This is pronounced bound if
#if DEBUGMOOD
            // This message will show if you have defined DEBUGMOOD in the start of your file
            // If you commented that line from the start of the file, the other message will be shown not this one.
            Console.WriteLine("This message will show only if you are defining DEBUGMOOD");
#else
            Console.WriteLine("This message will show only if you are not defining DEBUGMOOD");
#endif
#if JOE
            Console.WriteLine("Joe was here");
#else
            Console.WriteLine("Joe was not here");
#endif
            Console.Read();
        }
    }
}
