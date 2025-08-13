using PERFISOFT.VENTASPLATFORM.DOMAIN.Cloudinary;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.SERVICES.CloudinaryService
{
    public interface ICloudinaryService
    {
        Task<SubirImagenResponse> UploadImageAsync(string name, string base64Image, string subFolder);

        Task<SubirImagenResponse> UploadFileAsync(string name, string base64File, string subFolder);

        Task DeleteImageAsync(string publicId);
    }
}
