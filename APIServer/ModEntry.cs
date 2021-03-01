using APIServer.core;
using APIServer.injections;
using APIServer.util;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIServer
{
    public class ModEntry : Mod
    {

        private APIServer server;

        public override void Entry(IModHelper helper)
        {
            try
            {
                ConfigModel config = helper.Data.ReadJsonFile<ConfigModel>("config.json") ?? new ConfigModel();

                MonitorLog.Monitor = this.Monitor;
                Log.WriteDebugLog = config.WriteDebugLog;

                GameJS.Mod = this;
                Game1.input = new SInputStateExtended(Game1.input);

                server = new APIServer();
                server.LoadDefaultHandlers();
                server.Listen(config.Host, config.Port);

                this.Monitor.Log($"APIServer started in ${config.Host}:${config.Port}");
                helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
                helper.Events.Multiplayer.ModMessageReceived += OnModMessageReceivedEventArgs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OnUpdateTicked(object sender, UpdateTickedEventArgs args)
        {
            foreach (CharacterWalker walker in GameJS.WalkerMap().Values)
            {
                walker.Update();
            }
        }

        private void OnModMessageReceivedEventArgs(object sender, ModMessageReceivedEventArgs args)
        {
            Console.WriteLine("Type message: " + args.Type);
            Console.WriteLine("From player: " + args.FromPlayerID);
        }

    }
}
