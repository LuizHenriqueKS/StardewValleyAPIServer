using APIServer.core;
using APIServer.injections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewValley;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace APIServer.util
{
    public class GameJS
    {

        public static ModEntry Mod;
        private static Dictionary<Character, CharacterWalker> walkerMap = new Dictionary<Character, CharacterWalker>();

        public static void Prepare(JSEngine engine)
        {
            engine.AddType(typeof(Game1));
            engine.AddType(typeof(StardewModdingAPI.Context));
            engine.AddType(typeof(GameEvents));
            engine.AddObject("mod", Mod);
            engine.AddType(typeof(GameJS));
            engine.AddType(typeof(Vector2));
            //engine.AddType(typeof(NetVector2));
            engine.AddType(typeof(Point));
            engine.AddType(typeof(NetPosition));
            engine.AddType(typeof(PathFindController));
            engine.AddType(typeof(ButtonState));
            engine.AddType(typeof(Mouse));
            engine.AddType(typeof(MouseState));
            engine.AddType(typeof(KeyboardState));
            engine.AddType(typeof(Keyboard));
        }

        public static void SimulateMouse(MouseState state)
        {
            Type inputType = Game1.input.GetType();
            PropertyInfo propInfo = (PropertyInfo)inputType.GetProperty("_currentMouseState", BindingFlags.NonPublic);
            propInfo.SetValue(Game1.input, propInfo);
        }

        public static CharacterWalker GetWalker(Character character)
        {
            if (!walkerMap.ContainsKey(character))
            {
                walkerMap.Add(character, new CharacterWalker(character));
            }
            return walkerMap[character];
        }

        public static Dictionary<Character, CharacterWalker> WalkerMap()
        {
            return walkerMap;
        }

        public static SInputStateExtended Input
        {
            get
            {
                return ((SInputStateExtended)Game1.input);
            }
        }

    }
}
