using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ienumerable_and_linq
{
    /*
        As long as a collection implements the IEnumerable<T> interface, you can use LINQ queries with it.
        But LINQ also lets you work with more than just collections. 
        You can use the same queries to pull data from a database, or even an XML document. 
         */
    class Program
    {
        static void Main(string[] args)
        {
            var testLinq = new TestLinq();
            // Now the testLinq can see all the linq extension methods, try it if you want.
            //testLinq.
        }
    }

    public class TestLinq : IEnumerable<string>
    {
        public IEnumerator<string> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
