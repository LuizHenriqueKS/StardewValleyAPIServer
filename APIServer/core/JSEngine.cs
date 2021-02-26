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
        private readonly Dictionary<string, object> refMap;

        private int lastRefId;

        public JSEngine(APIClient client)
        {
            this.Client = client;
            this.engine = new V8ScriptEngine();
            this.refMap = new Dictionary<string, object>();

            AddType(typeof(Log));
            AddObject("client", client);
            AddObject("engine", this);
            Prepare();
        }

        public object Evaluate(string script)
        {
            string scriptBuilded = BuildScript(script);
            this.engine.Execute(scriptBuilded);
            return this.engine.Evaluate("main()");
        }

        public void Execute(string script)
        {
            string scriptBuilded = BuildScript(script);
            this.engine.Execute(scriptBuilded);
            this.engine.Execute("main();");
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

        public string AddReference(object value)
        {
            string name = $"ref#{lastRefId++}";
            this.refMap.Add(name, value);
            return name;
        }

        public object GetReference(string name)
        {
            return this.refMap[name];
        }

        private String BuildScript(string script)
        {
            return "function main(){ " + script + " }";
        }

        private void Prepare()
        {
            try
            {
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
