using APIServer.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.handler.events
{
    public class ButtonPressedEventHandler : IEventHandler
    {
        public bool CanFire(EventHandlerHelper helper)
        {
            return helper.Event is ButtonPressedEvent;
        }

        public void Fire(EventHandlerHelper helper)
        {
            foreach(Request request in helper.Server.ListRequests(GetType()))
            {
                request.Reply(enums.ResponseType.Ok, helper.Event);
            }
        }

        public string GetName()
        {
            return "ButtonPressedEvent";
        }
    }
}
