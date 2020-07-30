using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Net.Core.API.Serverless.App.Interfaces;
using Net.Core.API.Serverless.App.Util;

namespace Net.Core.API.Serverless.App.Handlers
{
    public class CoinHandler : Functions
    {
        private readonly ICoinService _coinService;

        public CoinHandler() : base()
        {
            _coinService = _serviceProvider.GetService<ICoinService>();
        }

        public APIGatewayProxyResponse Post(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.LogLine("Request incoming - post");
            var result = _coinService.Add(request.Body);

            return new ResponseProxy().ResponseStatus(result);
        }

        public APIGatewayProxyResponse Get(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.LogLine("Request incoming - get");
            var result = _coinService.GetCoinsByOrigin(request.QueryStringParameters);

            return new ResponseProxy().ResponseStatus(result);
        }

        public APIGatewayProxyResponse Delete(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.LogLine("Request incoming - deletessss dss");
            var result = _coinService.DeleteCoinByOrigin(request.PathParameters);

            return new ResponseProxy().ResponseStatus(result);
        }
    }
}
