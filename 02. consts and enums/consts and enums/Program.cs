using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consts_and_enums
{
    class Program
    {
        // the enumeration is a punch of related consts
        // What is the diffrence between "enum" & "Enum", the Enum is an abstract class that has alot of useful helper methods like parse and getNames ...
        enum Temperature
        {
            HOT = 100,
            FREEZING = 22
        }

        // the default enum values starts from 0, and by default they are int and if you want to change that
        // but you can only use the following data types
        // byte, sbyte, short, ushort, int, uint, long and ulong 
        // because they are numeric in nature
        // you can't create a string enum for example or double
        enum TemperatureByte : byte
        {
            HOT = 100,
            FREEZING = 22
        }

        static void Main(string[] args)
        {
            // const is used for the values that will not change over the app
            const int FREEZING = 22;
            const string NAME = "Ahmad";
            int myTemp = 22;
            
            if (myTemp == FREEZING)
            {
                Console.WriteLine("This const is working!!");
            }

            if (myTemp == (int)Temperature.FREEZING)
            {
                Console.WriteLine("The enum is working!!");
            }
            Console.Read();
        }
    }
}
