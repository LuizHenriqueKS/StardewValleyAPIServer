using APIServer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class EventBus<T>
    {
        public readonly ClientEventInfo EventInfo;

        public EventBus(ClientEventInfo eventInfo)
        {
            this.EventInfo = eventInfo;
        }

        public void FireEvent(Object sender, T eventArgs)
        {
            EventInfo.Request.Reply(enums.ResponseType.Event, new EventModel(sender, eventArgs));
        }
    }
}
