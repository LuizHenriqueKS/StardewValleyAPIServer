using APIServer.core;
using APIServer.model;
using APIServer.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.converter.response
{
    public class HostTypeConverter : IConverter
    {
        public bool CanConvert(object val)
        {
            return ClearScriptUtils.IsHostType(val);
        }

        public object Convert(object val)
        {
            MemberInfo typeInfo = val.GetType().GetMember("Type")[0];
            PropertyInfo propertyInfo = (PropertyInfo)typeInfo;
            Type type = (Type)propertyInfo.GetValue(val);
            HostTypeModel model = new HostTypeModel();
            model.Name = type.Name;
            model.FullName = type.FullName;
            return model;
        }
    }
}
