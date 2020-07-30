using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;

using Net.Core.API.Serverless.App;

namespace Net.Core.API.Serverless.App.Tests
{
    public class FunctionTest
    {
        public FunctionTest()
        {
        }

        [Fact]
        public void TetGetMethod()
        {
            Console.WriteLine("Ok");
            //TestLambdaContext context;
            //APIGatewayProxyRequest request;
            //APIGatewayProxyResponse response;

            //Functions functions = new Functions();


            //request = new APIGatewayProxyRequest();
            //context = new TestLambdaContext();
            //response = functions.Get(request, context);
            //Assert.Equal(200, response.StatusCode);
            //Assert.Equal("Hello AWS Serverless", response.Body);
        }
    }
}
