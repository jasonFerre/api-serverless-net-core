using Net.Core.API.Serverless.App.Commands.Coin;
using Net.Core.API.Serverless.App.Interfaces;
using Net.Core.API.Serverless.App.Util;
using Net.Core.API.Serverless.Domain.Entities;
using Net.Core.API.Serverless.Domain.Extensions;
using Net.Core.API.Serverless.Infrastructure.AWS.S3.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Net.Core.API.Serverless.App.Services
{
    public class CoinService : ICoinService
    {
        private readonly IS3Helper _s3Helper;
        private readonly string BucketName = "tuto-itau-bkt-dev";

        public CoinService(IS3Helper iS3Helper)
        {
            _s3Helper = iS3Helper;
        }

        public Response Add(string body)
        {
            try
            {
                Console.WriteLine($"body request \n {body}");
                var coinBody = new Coin(JsonConvert.DeserializeObject<PutCoinCommand>(body));
                var result = _s3Helper.Put(BucketName, $"NetCore/{coinBody.Origin}/{coinBody.CoinName}", JsonConvert.SerializeObject(coinBody));
                
                return new Response() { Body = coinBody, HttpStatusCode = HttpStatusCode.Created };
            }
            catch(DomainException ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
                return new Response() { Body = new { Error = ex.Message, TypeError = "DomainValidation" }, HttpStatusCode = HttpStatusCode.BadRequest };
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
                return new Response() { Body = new { Error = ex.Message, TypeError = "Error General" }, HttpStatusCode = HttpStatusCode.InternalServerError };
            }
        }

        public Response GetCoinsByOrigin(IDictionary<string, string> parameters)
        {
            try
            {
                Console.WriteLine($"parameters request \n {parameters}");
                if (parameters == null || !parameters.ContainsKey("Origin"))
                    return new Response() { Body = "", HttpStatusCode = HttpStatusCode.BadRequest };

                var result = _s3Helper.GetByKeyTextPlain<Coin>(BucketName, $"NetCore/{parameters["Origin"]}/", "/");
                if (result != null)
                    return new Response() { Body = result, HttpStatusCode = HttpStatusCode.OK };

                return new Response() { Body = {}, HttpStatusCode = HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                return new Response() { Body = JsonConvert.SerializeObject(new { Error = ex.Message, TypeError = "Error General" }), HttpStatusCode = HttpStatusCode.InternalServerError };
            }
        }

        public Response DeleteCoinByOrigin(IDictionary<string, string> parameters)
        {
            try
            {
                Console.WriteLine($"parameters request \n {parameters}");
                if (parameters == null || !parameters.ContainsKey("Origin"))
                    return new Response() { Body = "", HttpStatusCode = HttpStatusCode.BadRequest };

                if (_s3Helper.Delete(BucketName, $"NetCore/{parameters["Origin"]}"))
                {
                    Console.WriteLine("Deleted with success");
                    return new Response() { Body = {}, HttpStatusCode = HttpStatusCode.NoContent };
                }

                Console.WriteLine("Deleted failed");
                return new Response() { Body = new { Error = "Delete failed object not found" }, HttpStatusCode = HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
                return new Response() { Body = new { Error = ex.Message, TypeError = "Error General" }, HttpStatusCode = HttpStatusCode.InternalServerError };
            }
        }

        //public Response Update()
        //{

        //}
    }
}
