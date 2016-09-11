using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nbray
{
    public static class DateFormat
    {
        /// <summary>
        /// 将当前 <see cref="System.DateTime"/> 对象的值转换为其等效的全球化字符串表示形式。
        /// </summary>
        /// <param name="date">需要转换的 <see cref="System.DateTime"/> 对象。</param>
        /// <returns>一个字符串，它包含当前 <see cref="System.DateTime"/> 对象的全球化字符串表示形式。</returns>
        public static string ToGlobalizationString(this DateTime date)
        {
            return date.ToString("dddd,dd MMMM,yyyy", new DateTimeFormatInfo());
        }
    }
}
