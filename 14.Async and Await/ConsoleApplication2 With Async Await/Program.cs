using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2_With_Async_Await
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------Before calling the method------------");
            HelperMethod();
            Console.WriteLine("--------After calling the method------------");
            Console.ReadLine();
        }

        static async void HelperMethod()
        {
            // you can do the same using Task, the result will be the same
            //Task<string> longRunningTask = DownloadFromWebService();
            //var result = await longRunningTask;
            var result = await DownloadFromWebService();

            // the UI now is not freezing, you can write what you want
            Console.WriteLine(result);
        }

        static async Task<string> DownloadFromWebService()
        {
            Console.WriteLine("--------Before calling the sleep------------");
            // simulate delay for 5 seconds
            await Task.Delay(5000);
            Console.WriteLine("--------After calling the sleep------------");
            return "done";
        }
    }
}
