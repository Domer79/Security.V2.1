using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SecurityHttp.Helpers
{
    public class ThrowsHelper
    {
        public static Exception WebException(string message, WebException e)
        {
            var response = e.Response as HttpWebResponse;
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var str = sr.ReadToEnd();
                var webException = new WebException($"{message}. {str}", WebExceptionStatus.UnknownError);
                if (response != null)
                {
                    webException.Data["StatusCode"] = response.StatusCode;
                    webException.Data["StatusDescription"] = response.StatusDescription;
                }

                return webException;
            }
        }
    }
}
