using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace remove_item_from_list_foreach
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<string> {"J", "c", "d", "c", "f"};

            // delete using Linq
            //list.RemoveAll(e => e.Equals("c"));

            // using for loop
            
            //for (int i = 0; i < list.Count; i++)
            //{
            //    if (list[i] == "c")
            //    {
            //        list.RemoveAt(i);
            //    }
            //}

            // the .ToList() here is very important, because you can't delete from list while you looping on it
            // so .ToList() will create another list to loop through it, while you are deleting from the original list
            foreach (var item in list.ToList())
            {
                if (item.Equals("d"))
                {
                    list.Remove(item);
                }
            }

            // show list using linq
            // two ways to do that

            //list.ForEach(e => Console.WriteLine(e));
            list.ForEach(Console.WriteLine);

            Console.Read();
        }
    }
}
