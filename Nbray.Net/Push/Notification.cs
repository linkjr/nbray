using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nbray.Net.Push
{
    public class Notification
    {
        /// <summary>
        /// 获取或设置消息类型。
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        /// 获取或设置标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置内容。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置通知栏图标。
        /// </summary>
        public IconType Icon_Type { get; set; }

        /// <summary>
        /// 获取或设置图标地址。（应用内图标文件名或者下载图标的url地址）
        /// </summary>
        public string Icon_Res { get; set; }

        /// <summary>
        /// 获取或设置自定义参数。
        /// </summary>
        public Dictionary<string, object> Args { get; set; }

        /// <summary>
        /// 获取或设置打开的页面。
        /// </summary>
        public string Activity { get; set; }
    }
}
