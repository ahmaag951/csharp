using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stack_and_queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack stack = new Stack();
            stack.Push("1");
            stack.Push("2");
            stack.Push("3");
            stack.Push("4");

            stack.Pop(); // Will remove the last item, because stack is "LIFO" Like the dishes
            Console.WriteLine(stack.Peek());
            // Will call the last item without removing it from the stack

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-------------------------------");
            ////////////////////////////////////////////////////////////
            Queue queue = new Queue();
            queue.Enqueue("1");
            queue.Enqueue("2");
            queue.Enqueue("3");
            queue.Enqueue("4");

            queue.Dequeue(); // Will remove the last item, because stack is "LIFO" Like the dishes
            Console.WriteLine(queue.Peek());
            // Will call the last item without removing it from the stack

            foreach (var item in queue)
            {
                Console.WriteLine(item);
            }
            Console.Read();
        }
    }
}
