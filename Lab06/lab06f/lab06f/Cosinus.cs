using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab06f
{
    public class Cosinus
    {
        public static bool IsIt(String func)
        {
            return (func == "cos");
        }

        public static String Diff(String func)
        {
            return "-sin";
        }
    }
}
