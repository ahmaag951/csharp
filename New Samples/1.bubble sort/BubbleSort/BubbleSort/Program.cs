namespace BubbleSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var array = new int[] { 5, 3, 8, 4, 2 };
            BubbleSort(array);
            Console.WriteLine("Sorted array using bubble sort: " + string.Join(", ", array));
            // the complexity of bubble sort is O(n^2) ==> n squared ==> n is the number of elements in the array. 
            // for our array of 5 elements, O(n²) = 5² = 25 is the upper bound
            // in the worst case and O(n) in the best case.
            // in our case, the worst case is when the array is sorted in reverse order, and the best case is when the array is already sorted.
        }

        static void BubbleSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        // Swap array[j] and array[j + 1]
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
    }
}
