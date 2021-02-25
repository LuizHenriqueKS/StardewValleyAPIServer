using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class CommandManager
    {
        public readonly APIServer Server;
        private readonly List<ICommandHandler> handlerList;

        public CommandManager(APIServer server)
        {
            this.Server = server;
            this.handlerList = new List<ICommandHandler>();
        }

        public void AddHandler(ICommandHandler handler)
        {
            this.handlerList.Add(handler);
        }

        public void Handle(CommandHandlerHelper helper)
        {
            foreach (ICommandHandler handler in handlerList)
            {
                if (handler.CanHandle(helper))
                {
                    handler.Handle(helper);
                }
            }
        }

    }
}
