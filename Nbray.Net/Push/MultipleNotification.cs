using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nbray.Net.Push
{
    /// <summary>
    /// 表示组成多个推送消息的模型。
    /// </summary>
    public class MultipleNotification : Notification
    {
        /// <summary>
        /// 获取或设置设备Token。
        /// </summary>
        public string Token { get; set; }
    }
}
