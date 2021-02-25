using APIServer.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.handler.command
{
    public class PingCommandHandler : ICommandHandler
    {
        public bool CanHandle(CommandHandlerHelper helper)
        {
            return helper.Command.Name.ToLower() == GetName();
        }

        public string GetName()
        {
            return "Ping";
        }

        public void Handle(CommandHandlerHelper helper)
        {
            helper.Reply(enums.ResponseType.Ok, "Pong");
        }
    }
}
