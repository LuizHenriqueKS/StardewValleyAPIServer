using APIServer.core;
using APIServer.util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.handler.command
{
    public class RunJSCommandHandler : ICommandHandler
    {
        public bool CanHandle(CommandHandlerHelper helper)
        {
            return HandlerUtils.CanHandleByName(helper, this);
        }

        public string GetName()
        {
            return "RunJS";
        }

        public void Handle(CommandHandlerHelper helper)
        {
            try
            {
                string script = helper.Command.Args["script"].ToString();
                object response = helper.Client.JSEngine.Evaluate(script);
                helper.Reply(enums.ResponseType.Response, response);
            } catch (Exception e)
            {
                helper.Reply(enums.ResponseType.Error, e);
            }
        }
    }
}
