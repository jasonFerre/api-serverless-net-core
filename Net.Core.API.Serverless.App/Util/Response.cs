using System;
using System.Net;

namespace Net.Core.API.Serverless.App.Util
{
    public class Response
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public dynamic Body { get; set; }
    }
}
