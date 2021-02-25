using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public interface ICommandHandler
    {
        String GetName();

        bool CanHandle(CommandHandlerHelper helper);

        void Handle(CommandHandlerHelper helper);

    }
}
