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
                server = new APIServer();
                server.Listen(config.Host, config.Port);
                this.Monitor.Log($"APIServer started in ${config.Host}:${config.Port}");
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs args)
        {
            if (Context.IsWorldReady)
            {
                ButtonPressedEvent evt = new ButtonPressedEvent(sender, args);
                server.FireEvent(evt);
            }
        }

    }
}
