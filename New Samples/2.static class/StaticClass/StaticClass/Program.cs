namespace StaticClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // A static class is used when you want a class to hold helper/shared methods
            // or data and you don’t need to create objects from it.
            int result = MathHelper.Add(2, 3);
            Console.WriteLine($"The result of adding 2 and 3 is: {result}");
        }
    }

    public static class MathHelper
    {
        public static int Add(int a, int b)
        {
            return a + b;
        }

        // static class cannot have instance constructors, this will cause a compile-time error
        //public MathHelper()
        //{

        //}
    }

    public abstract class MyAbstractClass
    {
        // you can have static methods in an abstract class, but you can't have static abstract methods, so you have to implement the static method in the abstract class itself
        // you can't create static abstract methods in an abstract class, because it's static and it will be called by the class name, so the implementation has to be ready
        public static void MyAbstractMethod()
        {
            Console.WriteLine("Hello");
        }
    }
}
