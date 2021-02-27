using APIServer.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.model
{
    public class ClientEventInfo
    {
        public int Id;
        public Request Request;
        public Type Type;

        public Delegate Delegate;
        public object Target;
        public EventInfo EventInfo;

        public object[] DelegateArgs;

        public object bus;
    }
}
