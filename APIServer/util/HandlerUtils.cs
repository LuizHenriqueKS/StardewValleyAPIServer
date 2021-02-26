using APIServer.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.util
{
    public class HandlerUtils
    {
        public static bool CanHandleByName(CommandHandlerHelper helper, ICommandHandler handler)
        {
            return helper.Command.Name.ToLower() == handler.GetName().ToLower();
        }
    }
}
