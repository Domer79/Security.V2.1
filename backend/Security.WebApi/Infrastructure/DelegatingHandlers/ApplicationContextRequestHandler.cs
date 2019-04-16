using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Security.WebApi.Infrastructure.DelegatingHandlers
{
    public class ApplicationContextRequestHandler : DelegatingHandler
    {
        public static string ApplicationName { get; set; }
        private static string[] _repositories = new string[] {"roles", "secobjects", "grants", "memberroles", "policy", "common"};

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.Segments.Length >= 4)
                if (_repositories.Contains(request.RequestUri.Segments[3].Replace("/", "")))
                {
                    ApplicationName = request.RequestUri.Segments[2].Replace("/", "");
                }

            // log request body
            if (request.Content != null)
            {
                string requestBody = await request.Content.ReadAsStringAsync();
                Trace.WriteLine(requestBody);
            }

            // let other handlers process the request
            var result = await base.SendAsync(request, cancellationToken);

            if (result.Content != null)
            {
                // once response body is ready, log it
                var responseBody = await result.Content.ReadAsStringAsync();
                Trace.WriteLine(responseBody);
            }

            return result;
        }
    }
}