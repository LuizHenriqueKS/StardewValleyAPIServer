using APIServer.util;
using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class JSEngine
    {
        public APIClient Client;

        private readonly V8ScriptEngine engine;

        public JSEngine(APIClient client)
        {
            this.Client = client;
            this.engine = new V8ScriptEngine();
            this.engine.AllowReflection = true;

            AddObject("client", client);
            Prepare();
        }

        public object Evaluate(Request request, string script)
        {
            this.engine.AddHostObject("request", request);
            string scriptBuilded = BuildScript(script);
            return this.engine.Evaluate(scriptBuilded);
        }

        public void Execute(Request request, string script)
        {
            this.engine.AddHostObject("request", request);
            string scriptBuilded = BuildScript(script);
            this.engine.Execute(scriptBuilded);
        }

        public void AddType(Type type)
        {
            if (type != null)
            {
                this.engine.AddHostType(type);
            }
        }

        public void AddObject(string name, object value)
        {
            if (value != null)
            {
                this.engine.AddHostObject(name, value);
            }
        }

        public object getArrayValue(Object source, Object key)
        {
            var property = source.GetType().GetProperty("Item");
            var value = property.GetValue(source, new[] { key });
            return value;
        }

        private String BuildScript(string script)
        {
            return "(function (){ " + script + " })();";
        }

        private void Prepare()
        {
            try
            {
                this.engine.Execute("const refs = {};");
                AddType(typeof(Test));
                AddType(typeof(Log));
                AddObject("engine", this);
                GameJS.Prepare(this);
            }
            catch (FileNotFoundException)
            {

            }
            catch (TypeLoadException)
            {

            }
        }

    }
}
