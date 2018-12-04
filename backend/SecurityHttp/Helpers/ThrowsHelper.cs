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

            if (response == null)
                return e;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var str = sr.ReadToEnd();
                var ex = new WebException($"{message}. {str}", e, e.Status, e.Response);
                return ex;
            }
        }
    }
}
