namespace single_or_default_and_first_or_default
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var list = new List<int> { 1, 1, 2, 3, 4, 5 };

            var firstOrDefault = list.FirstOrDefault(x => x == 1);

            // this line will throw an exception because there are multiple elements that match the predicate
            // single or default will throw an exception if there are multiple elements that match the predicate
            // we use single or default when we want to gurantee that only one element to match the predicate
            var singleOrDefault = list.SingleOrDefault(x => x == 1);
        }
    }
}
