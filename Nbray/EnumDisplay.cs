using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    }
}
