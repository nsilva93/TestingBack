using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace TestingBack.SERVICE.Service.AWS
{
    public class AwsArchivosService
    { 
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _amazonS3Client;

        public AwsArchivosService(IConfiguration configuration, IAmazonS3 amazonS3)
         {
            _configuration = configuration;
            _amazonS3Client = amazonS3;
         }

        private string _BucketName => _configuration["AwsS3Archivos:BucketName"];
        private string _awsAccessKey => _configuration["AwsS3Archivos:AWSAccessKey"];
        private string _awsSecretKey => _configuration["AwsS3Archivos:AWSSecretKey"];

        public async Task<bool> SubirArchivoBucket(IFormFile file, string filePath)
        {
            var objectRequest = new PutObjectRequest()
            {
                BucketName = _BucketName,
                ContentType = file.ContentType,
                Key = filePath,
                InputStream = file.OpenReadStream(),
                CannedACL = S3CannedACL.BucketOwnerFullControl
            };

            var response = await _amazonS3Client.PutObjectAsync(objectRequest);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Successfully uploaded {filePath} to {_BucketName}.");
                return true;
            }
            else
            {
                Console.WriteLine($"Could not upload {filePath} to {_BucketName}.");
                return false;
            }

        }


        public async Task<byte[]> DescragarArchivoBucket(string objectKey)
        {
            MemoryStream ms = null;

            try
            {
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = _BucketName,
                    Key = objectKey
                };

                using (var response = await _amazonS3Client.GetObjectAsync(getObjectRequest))
                {
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        using (ms = new MemoryStream())
                        {
                            await response.ResponseStream.CopyToAsync(ms);
                        }
                    }
                }

                if (ms is null || ms.ToArray().Length < 1)
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", objectKey));

                return ms.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> FirmarURLArchivo(string objectKey)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _BucketName,
                Key = objectKey,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddSeconds(60)
            };

            return _amazonS3Client.GetPreSignedURL(request);
        }

        public async Task<bool> EliminarArchivo(string objectKey, string versionId)
        {
            IAmazonS3 s3Client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, Amazon.RegionEndpoint.USEast1);
            var request = new DeleteObjectRequest()
            {
                BucketName = _BucketName,
                Key = objectKey,
            };

            if (!string.IsNullOrEmpty(versionId))
                request.VersionId = versionId;

            await s3Client.DeleteObjectAsync(request);

            return true;
        }

        public async Task<bool> VerificarSiExisteArchivo(string objectKey, string versionId)
        {
            try
            {
                IAmazonS3 s3Client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, Amazon.RegionEndpoint.USEast1);

                GetObjectMetadataRequest request = new GetObjectMetadataRequest()
                {
                    BucketName = _BucketName,
                    Key = objectKey,
                    VersionId = !string.IsNullOrEmpty(versionId) ? versionId : null
                };

                var response = s3Client.GetObjectMetadataAsync(request).Result;

                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException is AmazonS3Exception awsEx)
                {
                    if (string.Equals(awsEx.ErrorCode, "NoSuchBucket"))
                        return false;

                    else if (string.Equals(awsEx.ErrorCode, "NotFound"))
                        return false;
                }

                throw;
            }
        }
    }
}
