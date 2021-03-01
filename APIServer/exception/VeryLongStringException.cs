using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.exception
{
    public class VeryLongStringException: Exception
    {
        public VeryLongStringException(int size): base($"Invalid size: {size}")
        {
        }
    }
}
