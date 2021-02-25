using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class EventManager
    {
        public readonly APIServer Server;
        private readonly List<IEventHandler> handlerList;

        public EventManager(APIServer server)
        {
            this.Server = server;
            this.handlerList = new List<IEventHandler>();
        }

        public void AddHandler(IEventHandler handler)
        {
            this.handlerList.Add(handler);
        }

        public void Fire(EventHandlerHelper helper)
        {
            foreach (IEventHandler handler in handlerList)
            {
                if (handler.CanFire(helper))
                {
                    handler.Fire(helper);
                }
            }
        }

    }
}
