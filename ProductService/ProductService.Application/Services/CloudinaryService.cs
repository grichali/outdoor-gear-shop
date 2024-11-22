using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Services
{
    public class CloudinaryService : Interfaces.ICloudinary
    {
        private readonly Cloudinary _cloudinary;
        CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }
        public async Task<string> UploadImageAsync(Stream fileStream)
        {
            string uniqueFileName = Guid.NewGuid().ToString();
            var uploadParam = new ImageUploadParams
            {
                File = new FileDescription(uniqueFileName,fileStream),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,

            };
            ImageUploadResult imageResult = await _cloudinary.UploadAsync(uploadParam);
            if(imageResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return imageResult.SecureUrl.ToString();
            }
            throw new Exception("Failed to upload the image" + imageResult.Error?.Message);
        }
    }
}
