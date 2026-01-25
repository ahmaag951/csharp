using System;

namespace test_extension_method
{
    public static class StringHandler
    {
        public static string AddABC(this string source)
        {
            return source + "abc";
        }
    }
}
