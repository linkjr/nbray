using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nbray.Net.Push
{
    /// <summary>
    /// 表示组成所有推送消息的模型。
    /// </summary>
    public class AllNotification : Notification
    {
        /// <summary>
        /// 获取或设置循环任务执行的次数，取值[1,15]。
        /// </summary>
        public int? LoopTimes { get; set; }

        /// <summary>
        /// 获取或设置循环任务的执行间隔，以天为单位，取值[1, 14]。
        /// <para>loop_times和loop_interval一起表示任务的生命周期，不可超过14天。</para>
        /// </summary>
        public int? LoopInterval { get; set; }
    }
}
