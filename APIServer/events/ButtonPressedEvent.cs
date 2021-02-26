using StardewModdingAPI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer
{
    public class ButtonPressedEvent : APIEvent
    {
        public object Sender;
        public object Args;

        public ButtonPressedEvent(object sender, ButtonPressedEventArgs args)
        {
            this.Sender = sender;
            this.Args = args;
        }
    }
}
