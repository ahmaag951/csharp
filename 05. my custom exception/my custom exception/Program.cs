using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_custom_exception
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                throw new NoJoesException();
            }
            catch (NoJoesException noJoes)
            {
                Console.WriteLine(noJoes.Message);
                Console.WriteLine("If you need any help please visit: " + noJoes.HelpLink);
            }
            finally
            {
                Console.Read();
            }
        }
    }

    internal class NoJoesException : Exception
    {
        // You don't have to do this but this is the only way to set the exception message (by the ctor)
        public NoJoesException()
            : base("I'm the exception message")
        {
            // You can't do like this, because the message property has no setter
            //this.Message = "I'm the exception message";
            // also there are some properties that you can set and get like
            this.HelpLink = "http://www.google.com";
        }
    }
}
