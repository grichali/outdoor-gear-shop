using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Domain.Entities;

namespace ProductService.Domain.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> AddAsync(Image image);
        Task<bool> DeleteAsync(string id);
        Task<Image> GetByIdAsync(string imageId);
        Task<Image> UpadateAsync(Image image);
    }
}
