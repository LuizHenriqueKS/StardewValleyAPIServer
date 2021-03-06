﻿using APIServer.core;
using APIServer.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.handler.command
{
    public class PingCommandHandler : ICommandHandler
    {
        public bool CanHandle(CommandHandlerHelper helper)
        {
            return HandlerUtils.CanHandleByName(helper, this);
        }

        public string GetName()
        {
            return "Ping";
        }

        public void Handle(CommandHandlerHelper helper)
        {
            helper.Reply(enums.ResponseType.Response, "Pong");
        }
    }
}
