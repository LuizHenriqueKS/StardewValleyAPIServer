using APIServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public abstract class EventCommandHandler<T> : ICommandHandler where T:IEventHandler 
    {

        public bool CanHandle(CommandHandlerHelper helper)
        {
            string commandName = "listen" + typeof(T).Name.Replace("Handler", "");
            return commandName.ToLower() == helper.Command.Name.ToLower();
        }

        public string GetName()
        {
            return typeof(T).Name.Replace("EventHandler", "");
        }

        public void Handle(CommandHandlerHelper helper)
        {
            helper.Server.AddRequest(typeof(T), helper.Request);
            helper.Reply(enums.ResponseType.Accepted, new CommandAcceptedModel(GetName()));
        }
    }
}
