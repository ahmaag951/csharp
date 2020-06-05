using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace to_list_importance
{
    class Program
    {
        static void Main(string[] args)
        {

            var list = new List<string>() { "a", "b", "c", "d" };
            // Kindly try to debug this line of code, and you will find that this variable contains only a query
            // But the .ToList() method will convert that linq var into a List<T> object
            var edits = from item in list
                        select item + "1 ";

            var editsList = edits.ToList();
        }
    }
}
