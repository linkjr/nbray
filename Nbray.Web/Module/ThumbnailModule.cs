using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Nbray.Web.Module
{
    public class ThumbnailModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            var thumbnailPhysicalPath = application.Request.PhysicalPath;
            if (string.IsNullOrEmpty(thumbnailPhysicalPath))
                return;
            //MimeMapping类基于.net4.5+
            //参考http://blog.useasp.net/archive/2013/08/23/the-method-to-retrieve-file-content-type-or-mime-type-information-string.aspx
            var mimeType = MimeMapping.GetMimeMapping(thumbnailPhysicalPath);
            if (!mimeType.Contains("image"))//非图片类型文件，不作处理
                return;
            if (File.Exists(thumbnailPhysicalPath))//缩略图文件存在，不作处理
                return;
        }
    }
}
