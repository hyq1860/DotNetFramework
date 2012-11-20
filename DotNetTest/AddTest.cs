using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTest
{
    public class AddTest:IAdd
    {
        public void Alert(string message)
        {
            Console.Write(message);
        }
    }
}
