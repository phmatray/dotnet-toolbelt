using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHelpers
{
    public static class HelloHelper
    {
        public static string Hello()
            => "Hello SimpleHelper";

        public static string Echo(this string s)
            => s;
    }
}
