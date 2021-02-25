using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public interface IEventHandler
    {
        String GetName();

        bool CanFire(EventHandlerHelper helper);

        void Fire(EventHandlerHelper helper);

    }
}
