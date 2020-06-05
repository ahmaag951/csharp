using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace public_static_or_static_public
{
    public static class Helper1
    {
        public static string Id { get; set; }
    }

    static public class Helper2
    {
        static public string Id { get; set; }
    }
}
