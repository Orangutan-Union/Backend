using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace TECHUB.Service.Interfaces
{
    public interface IPhotoService
    {
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo, string publicId);
    }
}
