using APIServer.core;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.util
{
    public class GameJS
    {

        public static Mod Mod;

        public static void Prepare(JSEngine engine)
        {
            engine.AddType(typeof(Game1));
            engine.AddType(typeof(StardewModdingAPI.Context));
            engine.AddType(typeof(GameEvents));
            engine.AddObject("mod", Mod);
        }
    }
}
