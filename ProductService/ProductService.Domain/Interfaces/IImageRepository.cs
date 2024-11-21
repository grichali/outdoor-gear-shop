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
        Task<Image> GetByIdAsync(string id);
        Task<List<Image>> GetByProductIdAsync(string productId);
        Task AddAsync(Image image);
        Task DeleteAsync(string id);
    }
}
