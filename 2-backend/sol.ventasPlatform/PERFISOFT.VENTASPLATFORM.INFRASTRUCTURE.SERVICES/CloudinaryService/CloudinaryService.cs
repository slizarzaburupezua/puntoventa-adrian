using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using PERFISOFT.VENTASPLATFORM.DOMAIN.Cloudinary;
using System.Net;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.CloudinaryService
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private readonly string assetFolder = "VentasPlatformDemo";

        public CloudinaryService(IConfiguration configuration)
        {
            _configuration = configuration;

            var cloudName = _configuration["Cloudinary:CloudName"];
            var apiKey = _configuration["Cloudinary:ApiKey"];
            var apiSecret = _configuration["Cloudinary:ApiSecret"];

            _cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
        }

        public async Task<SubirImagenResponse> UploadImageAsync(string name, string base64Image, string subFolder)
        {
            var folderPath = string.IsNullOrWhiteSpace(subFolder) ? assetFolder : $"{assetFolder}/{subFolder}";

            var response = new SubirImagenResponse();

            byte[] imageBytes = Convert.FromBase64String(base64Image);

            using (var stream = new MemoryStream(imageBytes))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(name, stream),
                    AssetFolder = folderPath
                };

                var uploadResponse = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResponse.StatusCode.Equals(HttpStatusCode.OK))
                {
                    response.PublicId = uploadResponse.PublicId;
                    response.SecureURL = uploadResponse.SecureUrl.ToString();
                }
                else
                {
                    response.PublicId = string.Empty;
                }
            }

            return response;
        }

        public async Task<SubirImagenResponse> UploadFileAsync(string name, string base64File, string subFolder)
        {
            var folderPath = string.IsNullOrWhiteSpace(subFolder) ? assetFolder : $"{assetFolder}/{subFolder}";

            var response = new SubirImagenResponse();
            byte[] fileBytes = Convert.FromBase64String(base64File);

            using (var stream = new MemoryStream(fileBytes))
            {
                var uploadParams = new RawUploadParams()
                {
                    File = new FileDescription(name, stream),
                    Folder = folderPath,
                };

                var uploadResponse = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResponse.StatusCode == HttpStatusCode.OK)
                {
                    response.PublicId = uploadResponse.PublicId;
                    response.SecureURL = uploadResponse.SecureUrl.ToString();
                }
                else
                {
                    response.PublicId = string.Empty;
                }
            }

            return response;
        }
         
        public async Task DeleteImageAsync(string publicId)
        {
            var deleteRequest = new DeletionParams(publicId);
            var deleteResult = await _cloudinary.DestroyAsync(deleteRequest);

        }
    }
}
