using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class DefaultEventHandler<T> : IEventHandler where T : APIEvent
    {
        public bool CanFire(EventHandlerHelper helper)
        {
            return helper.Event is T;
        }

        public void Fire(EventHandlerHelper helper)
        { 
            foreach (Request request in helper.Server.ListRequests(GetType()))
            {
                request.Reply(enums.ResponseType.Event, helper.Event);
            }
        }

        public string GetName()
        {
            return typeof(T).Name;
        }
    }
}
