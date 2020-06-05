using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace array_list_and_list_and_their_data_types
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrayList = new ArrayList();
            var list = new List<int>();

            // In the array list you can add any data types, In the list you can't

            // This is in data structure called linked list
            arrayList.Add(5);
            arrayList.Add("name");

            list.Add(5);
            // this is error because the list is int only
            //list.Add("name");
        }
    }
}
