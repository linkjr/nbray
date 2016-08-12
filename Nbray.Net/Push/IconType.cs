using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nbray.Net.Push
{
    /// <summary>
    /// 表示通知栏图标是应用内图标还是上传图标。
    /// </summary>
    public enum IconType
    {
        /// <summary>
        /// 应用内图标。
        /// </summary>
        Application = 0,

        /// <summary>
        /// 上传图标。
        /// </summary>
        Upload = 1
    }
}
