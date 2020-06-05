using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yield_return
{
    class Program
    {
        static void Main(string[] args)
        {
            // the yield keyword allows us to return an enumerated value without breaking from the GetEnumerator method
            var list = getList(2, 6);
            foreach (var item in list)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();

            var list2 = getListUsingYield(2, 6);
            foreach (var item in list2)
            {
                Console.Write(item + " ");
            }
            Console.Read();
        }

        public static List<int> getList(int from, int to)
        {
            List<int> list = new List<int>();
            for (int i = from; i <= to; i++)
            {
                list.Add(i);
            }
            return list;
        }

        // This should return IEnumerable
        public static IEnumerable<int> getListUsingYield(int from, int to)
        {
            for (int i = from; i <= to; i++)
            {
                yield return i;
            }
        }
    }
}
