using Amazon.Lambda.Core;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net.Core.API.Serverless.App.Interfaces;
using Net.Core.API.Serverless.App.Services;
using Net.Core.API.Serverless.Infrastructure.AWS.S3;
using Net.Core.API.Serverless.Infrastructure.AWS.S3.Interfaces;
using System;
using System.IO;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Net.Core.API.Serverless.App
{
    public class Functions
    {
        private IServiceCollection _serviceCollection;
        protected IServiceProvider _serviceProvider;
        public IConfiguration _configuration;

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions()
        {
            Configure();
            Services();
        }

        private void Configure()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        private void Services()
        {
            if (_serviceCollection == null)
                _serviceCollection = new ServiceCollection();
            else
                return;

            //aws services
            _serviceCollection.AddDefaultAWSOptions(_configuration.GetAWSOptions());
            _serviceCollection.AddAWSService<IAmazonS3>();

            _serviceCollection.AddTransient<IS3Helper, S3Helper>();
            _serviceCollection.AddTransient<ICoinService, CoinService>();

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }
    }
}
