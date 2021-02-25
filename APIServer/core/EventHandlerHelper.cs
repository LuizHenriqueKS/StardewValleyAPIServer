using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class EventHandlerHelper
    {
        public readonly APIServer Server;
        public readonly APIEvent Event;

        public EventHandlerHelper(APIServer server, APIEvent _event){
            this.Server = server;
            this.Event = _event;
        }
    }
}
