using APIServer.model;
using APIServer.util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class ClientEventInfoFactory
    {
        public static ClientEventInfo Create(int id, Request Request, Object target, String eventName)
        {
            ClientEventInfo info = new ClientEventInfo();
            info.Id = id;
            info.Request = Request;
            info.Type = ClearScriptUtils.GetType(target);
            info.EventInfo = info.Type.GetEvent(eventName);
            Type eventArgsType = info.EventInfo.EventHandlerType.GenericTypeArguments[0];
            info.Target = ClearScriptUtils.IsHostType(target) ? null : target;
            info.Bus = Activator.CreateInstance(typeof(EventBus<>).MakeGenericType(eventArgsType), new object[] { info });
            info.Delegate = CreateDelegate(info);
            info.DelegateArgs = new object[1]{ info.Delegate };
            info.WriteLog = true;
            return info;
        }

        public static Delegate CreateDelegate(ClientEventInfo info)
        {
            MethodInfo metInf = info.Bus.GetType().GetMethod("FireEvent");
            Type tDelegate = info.EventInfo.EventHandlerType;
            return Delegate.CreateDelegate(tDelegate, info.Bus, metInf);
        }

    }
}
