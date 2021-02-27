using APIServer.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.util
{
    public class Test
    {
        public static int TEN = 10;
        public static event EventHandler<bool> BoolEventHandler;

        public static int Sum(int a, int b)
        {
            return a + b;
        }

        public static void Fire(bool arg)
        {
            BoolEventHandler?.Invoke(null, arg);
            Log.Info($"Testing: {arg}");
        }
    }
}
