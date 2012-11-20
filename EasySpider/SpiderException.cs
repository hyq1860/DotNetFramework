using System;
using System.Collections.Generic;
using System.Text;

namespace EasySpider
{
    class SpiderException:Exception
    {
        public SpiderException(string message):base
            (message)
        {
           
        }
    }
}
