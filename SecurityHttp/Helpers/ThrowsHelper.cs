using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SecurityHttp.Helpers
{
    public class ThrowsHelper
    {
        public static Exception WebException(string message, HttpWebResponse response = null)
        {
            var webException = new WebException(message, WebExceptionStatus.UnknownError);
            if (response != null)
            {
                webException.Data["StatusCode"] = response.StatusCode;
                webException.Data["StatusDescription"] = response.StatusDescription;
            }

            return webException;
        }
    }
}
