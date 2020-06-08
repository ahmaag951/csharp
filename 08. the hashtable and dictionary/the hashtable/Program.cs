using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace the_hashtable
{
    class Program
    {
        static void Main(string[] args)
        {
            // The hashtable is an example of using "Dictionary"
            // It's a structure that can map keys to values
            // Hash functions are used in hash tables, to quickly locate a data record given its search key.
            // A hash function is any function that can be used to map data of arbitrary size to data of fixed size.
            
            Hashtable hashtable = new Hashtable();
            
            hashtable.Add("key", "value");
            hashtable["key2"] = "value2";
            // Hash table is generic you can add strings and ints, but the dictionary is not
            hashtable["key3"] = 15;

            int count = hashtable.Count;
            bool b = hashtable.ContainsKey("key");
            bool b2 = hashtable.ContainsValue("value");

            Console.WriteLine(hashtable["key"]);
            Console.WriteLine(count);

            hashtable.Remove("key");
            count = hashtable.Count;
            Console.WriteLine(count);

            Console.WriteLine(b);
            Console.WriteLine(b2);
            Console.WriteLine(hashtable["key3"]);

            // Dictionary 

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("key", "value From Dictionary");
            //dictionary.Add("key", 15);//You can't do this because the dictionary is not generic

            Console.WriteLine(dictionary["key"]);
            Console.Read();
        }
    }
}
