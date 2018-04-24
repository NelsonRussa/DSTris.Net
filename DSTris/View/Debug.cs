using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSTris.View
{
    class Debug
    {
        public static void ShowMessage(string message, bool waitKey = false)
        {
            Console.WriteLine(message);

            if (waitKey)
                Console.ReadKey();
        }
    }
}
