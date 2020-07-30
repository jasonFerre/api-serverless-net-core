using Amazon.S3.Model;
using System.Collections.Generic;

namespace Net.Core.API.Serverless.Infrastructure.AWS.S3.Interfaces
{
    public interface IS3Helper
    {
        /// <summary>
        /// add a new object in s3 based in method parameters
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <param name="body"></param>
        /// <param name="contentType"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        PutObjectResponse Put(string bucketName, string key, string body, string contentType = "text/plain", string extension = ".txt");

        /// <summary>
        /// get the content for objects listed
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        List<T> GetByKeyTextPlain<T>(string bucketName, string key, string delimiter = null);

        /// <summary>
        /// list objects informations, like key tags 
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        ListObjectsV2Response ListObjectsByPrefixOrDelimiter(string bucketName, string key, string delimiter = null);

        /// <summary>
        /// delete an object at s3
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Delete(string bucketName, string key);
    }
}
