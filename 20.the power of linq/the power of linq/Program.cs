using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_power_of_linq
{
    class Program
    {
        static void Main(string[] args)
        {
            // Linq lets you write queries that do every complex things using very little code.
            var list = new List<string>() { "a", "b", "c", "d" };
            // Modify every item returned from the query
            var edits = from item in list
                        select item + "1 ";

            var edits2 = list.Select(r => r + "1 ");

            foreach (var item in edits)
            {
                Console.Write(item);
            }
            Console.WriteLine();
            foreach (var item in edits2)
            {
                Console.Write(item);
            }

            Console.WriteLine();
            // Another way by using aggregate
            string delimiter = ",";
            List<string> items = new List<string>() { "foo", "boo", "john", "doe" };
            // If you want to know what does workingSentence you can debug and you will see what it contains
            Console.WriteLine(items.Aggregate((workingSentence, next) => workingSentence + delimiter + next));

            Console.Read();
        }
    }
}
