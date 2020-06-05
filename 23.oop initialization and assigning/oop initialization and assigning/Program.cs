using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop_initialization_and_assigning
{
    class Program
    {
        static void Main(string[] args)
        {
            IAnimal duckAnimal = new Duck();
            duckAnimal.Voice();
            // in duckAnimal I can't see MethodForDuckOnly because it's initialized as an interface.

            Duck duck = new Duck();
            duck.Voice();
            duck.MethodForDuckOnly();
        }
    }

    interface IAnimal
    {
        void Voice();
    }

    class Duck : IAnimal
    {

        public void Voice()
        {
            Console.WriteLine("Duck Voice.");
        }

        public void MethodForDuckOnly()
        {
            Console.WriteLine("Duck Only.");
        }
    }
}
