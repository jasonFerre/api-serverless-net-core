using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Net.Core.API.Serverless.App.Util
{
    public class ResponseProxy
    {
        public APIGatewayProxyResponse ResponseStatus(Response response)
        {
            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)response.HttpStatusCode,
                Body = JsonConvert.SerializeObject(response.Body),                
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
    }
}
