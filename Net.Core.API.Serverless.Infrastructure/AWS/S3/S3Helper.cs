using Amazon.S3;
using Amazon.S3.Model;
using Net.Core.API.Serverless.Infrastructure.AWS.S3.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Net.Core.API.Serverless.Infrastructure.AWS.S3
{
    public class S3Helper : IS3Helper
    {
        private readonly IAmazonS3 _amazonS3;

        public S3Helper(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

        public bool Delete(string bucketName, string key)
        {
            try
            {
                var listObjects = ListObjectsByPrefixOrDelimiter(bucketName, key);
                var s3Objets = listObjects?.S3Objects?.FirstOrDefault();

                if (s3Objets == null)
                    return false;

                _amazonS3.DeleteObjectAsync(new DeleteObjectRequest()
                {
                    BucketName = s3Objets.BucketName,
                    Key = s3Objets.Key
                });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PutObjectResponse Put(string bucketName, string key, string body, string contentType = "text/plain", string extension = ".txt")
        {
            try
            {
                return _amazonS3.PutObjectAsync(new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = $"{key}{extension}",
                    ContentType = contentType,
                    ContentBody = body
                }).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> GetByKeyTextPlain<T>(string bucketName, string key, string delimiter = null)
        {
            try
            {
                var listObjects = ListObjectsByPrefixOrDelimiter(bucketName, key, delimiter);

                var resultList = new List<string>();
                foreach (var s3Object in listObjects.S3Objects)
                {
                    var objectRequest = _amazonS3.GetObjectAsync(new GetObjectRequest()
                    {
                        BucketName = s3Object.BucketName,
                        Key = s3Object.Key
                    }).Result;

                    if (objectRequest != null)
                        resultList.Add(new StreamReader(objectRequest.ResponseStream).ReadToEnd());
                }

                return resultList.Where(item => !string.IsNullOrEmpty(item)).Select(item => JsonConvert.DeserializeObject<T>(item)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public ListObjectsV2Response ListObjectsByPrefixOrDelimiter(string bucketName, string key, string delimiter = null)
        {
            try
            {
                var request = new ListObjectsV2Request() { BucketName = bucketName, Prefix = key };
                if (delimiter != null)
                    request.Delimiter = delimiter;

                var listObjectV2Response = new ListObjectsV2Response();
                do
                {
                    var response = _amazonS3.ListObjectsV2Async(request).Result;
                    if (response == null || response?.KeyCount == 0)
                        return listObjectV2Response;

                    listObjectV2Response.IsTruncated = response.IsTruncated;
                    listObjectV2Response.S3Objects.AddRange(response.S3Objects);

                } while (listObjectV2Response.IsTruncated);

                return listObjectV2Response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
