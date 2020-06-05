using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            DownloadFromWebService();
            // the UI will be stoped for 5 seconds, and you will notice that
            // when you write some texts on the console while you are running app,
            // what you are writing will not show unless the five seconds are finished.
            Console.ReadLine();
        }

        static void DownloadFromWebService()
        {
            // simulate delay for 5 seconds
            Thread.Sleep(5000);
        }
    }
}
