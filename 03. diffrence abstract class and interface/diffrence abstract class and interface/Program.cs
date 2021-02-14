using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace diffrence_abstract_class_and_interface
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. abstract class may contain implementation, interface contain only declarations no implementations
            // 2. the multiple inheritance, the class can inherit only one abstract class, but it can implement any number of interfaces
            // 3. abstract class can contain access modifires, interfaces can't and they are automatically public
            /*
             4. abstract class can contain any thing that the normal class can contain like 
             * fields, properties, constructors, destructors, methods, events, indexers 
             * and interfaces can't have fields or constructors and destructors because they are specific to a particular class
             */
        }
    }

    internal abstract class AbstractClass
    {
        // 1. The abstract class can contain method implementation
        public string Display()
        {
            return "Hello";
        }

    }

    interface IMyInterface
    {
        // 1. No method body, only method header
        // 3. No access modifires are allowed, they are by default public
        string Display();
    }
}
