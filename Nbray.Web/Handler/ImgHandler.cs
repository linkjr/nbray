using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Nbray.Web.Handler
{
    public class ImgHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                foreach (var domain in ConfigurationManager.AppSettings["domain"].Split(','))
                {
                    if (context.Request.UrlReferrer.AbsoluteUri.Contains(domain))
                    {
                        context.Response.ContentType = "image/gif";
                        context.Response.WriteFile(context.Request.FilePath);
                    }
                    else
                    {
                        context.Response.ContentType = "image/jpeg";
                        context.Response.WriteFile("error.jpg");
                    }
                }
            }
        }
    }
}
