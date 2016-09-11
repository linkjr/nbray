using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nbray
{
    public static class EnumDisplay
    {
        /// <summary>
        /// 返回指定枚举值的描述信息。
        /// </summary>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumVal)
        {
            var val = enumVal.ToString();
            var typ = enumVal.GetType();
            var fieldInfo = typ.GetField(enumVal.ToString());
            string result;
            if (fieldInfo == null)
                result = val;
            else
            {
                var objs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                    result = val;
                }
                else
                {
                    var da = (DescriptionAttribute)objs[0];
                    result = da.Description;
                }
            }
            return result;
        }

        /// <summary>
        /// 根据指定枚举类型返回对应的值和描述组成的字典。
        /// </summary>
        /// <param name="enumType">指定的枚举类型。</param>
        /// <returns></returns>
        public static Dictionary<int, string> GetEnumOptions(Type enumType)
        {
            var list = new Dictionary<int, string>();
            var fields = enumType.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                var fieldInfo = fields[i];
                if (fieldInfo.FieldType.BaseType != typeof(Enum))
                    continue;

                var value = fieldInfo.Name;
                var objs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs != null && objs.Length != 0)
                {
                    var da = (DescriptionAttribute)objs[0];
                    value = da.Description;
                }
                var key = (int)Enum.Parse(enumType, fieldInfo.Name);
                list.Add(key, value);
            }
            return list;
        }
    }
}
