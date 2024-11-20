using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Extensions
{
    public static class FileExtension
    {
    
        public static async Task<string> UploadProduit(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return "web host envirenement error";
            }

            try
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/product");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }
                
                return "images/product" + uniqueFileName;
            }
            catch (Exception ex)
            {
                return $"UploadImage Exception: {ex.Message}";

            }
        }
        public static async Task<string> UploadCategory(this IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {

            if (webHostEnvironment == null)
            {

                return "web host envirenement error";
            }

            try
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/category");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }
                
                return "images/category" + uniqueFileName;
            }
            catch (Exception ex)
            {
                return $"UploadImage Exception: {ex.Message}";

            }
        }
    }
}