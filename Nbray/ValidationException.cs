using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nbray
{
    /// <summary>
    /// 表示验证异常类。
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// 初始化 <c>ValidationException</c> 类的新实例。
        /// </summary>
        /// <param name="message"></param>
        public ValidationException(string message) : base(message) { }
    }
}
