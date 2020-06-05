using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_delegates
{
    // 1. Declaring delegate
    public delegate bool IsValid();
    public delegate void IsValid2();
    // now this delegate can hold any method that takes no parameters and return bool
    class Program
    {
        static void Main(string[] args)
        {
            // The delegate is a refrence to a method, it describe what kind of method can hold
            // 1. Declare a delegate
            // 2. Create a method to use the delegate
            // 3. Create one or more methods that matches the delegate's return value and parameters
            // so I create playAudio, and playVideo
            // 4. Instantiate the delegate
            // 5. Invoke the method through the delegate

            // step 4
            IsValid testMedia = PlayAudio;
            Console.WriteLine(TestResults(testMedia));

            testMedia = PlayVideo;
            Console.WriteLine(TestResults(testMedia));

            IsValid2 test = TestVoid;

            TestCallTheVoidDelegate(test);

            Console.Read();
        }

        
        // this is the step 2
        // now you can pass to this method any method that takes no parameters and return bool
        public static string TestResults(IsValid isValidDelegate)
        {
            // We treat it like a method, note the () after its name
            if (isValidDelegate() == true)
            {
                return "Yes, It's valid!!";
            }
            else
            {
                return "No Sorry, It's not valid!!";
            }
        }

        public static bool PlayAudio()
        {
            // suppose that some errors happened while playing audio
            Console.WriteLine("Error happend while playing audio");
            return false;
        }

        public static bool PlayVideo()
        {
            // suppose that no errors happened while playing video
            Console.WriteLine("No Errors happend while playing video");
            return true;
        }

        public static void TestVoid()
        {
            Console.WriteLine("Nothing... this method returns void");
        }

        public static void TestCallTheVoidDelegate(IsValid2 isValid2Delegate)
        {
            Console.WriteLine(isValid2Delegate);
        }
    }
}
