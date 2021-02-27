using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.model
{
    public class EventModel
    {
        public readonly object Sender;
        public readonly object Args;

        public EventModel(object sender, object args)
        {
            this.Sender = sender;
            this.Args = args;
        }
    }
}
