using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ploymorphysm_with_interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    internal class Test : IOne, ITwo
    {
        // No proplems will happen when a class implement two interfaces with similar method's names
        // But, you have to call it with the interface name, because it exists in the two interfaces with the same name
        void IOne.Print()
        {
            throw new NotImplementedException();
        }

        public void SpecialOne()
        {
            throw new NotImplementedException();
        }

        void ITwo.Print()
        {
            throw new NotImplementedException();
        }
    }

    interface IOne
    {
        void Print();
        void SpecialOne();
    }

    interface ITwo
    {
        void Print();
    }
}
