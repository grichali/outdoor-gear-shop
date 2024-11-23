using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MongoDB.Driver;
using ProductService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICloudinary = ProductService.Application.Interfaces.ICloudinary;

namespace ProductService.Application.Services
{
    public class CloudinaryService : ICloudinary
    {
        private readonly Cloudinary _cloudinary;
        CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<bool> DeleteImageAsync(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            DelResParams deleteParams = new DelResParams()
            {
                PublicIds = new List<string> { fileName },
                Type = "upload",
                ResourceType = ResourceType.Image
            };
            DelResResult result = await _cloudinary.DeleteResourcesAsync(deleteParams);
            if(result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
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
