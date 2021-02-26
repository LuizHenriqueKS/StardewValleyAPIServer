using APIServer.core;
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
    class ModEntry : Mod
    {

        private APIServer server;

        public override void Entry(IModHelper helper)
        {
            try
            {
                ConfigModel config = helper.Data.ReadJsonFile<ConfigModel>("config.json") ?? new ConfigModel();

                MonitorLog.Monitor = this.Monitor;
                Log.WriteDebugLog = config.WriteDebugLog;

                server = new APIServer();
                server.LoadDefaultHandlers();
                server.Listen(config.Host, config.Port);

                this.Monitor.Log($"APIServer started in ${config.Host}:${config.Port}");

                helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs args)
        {
            ButtonPressedEvent evt = new ButtonPressedEvent(sender, args);
            server.FireEvent(evt);
        }

    }
}
