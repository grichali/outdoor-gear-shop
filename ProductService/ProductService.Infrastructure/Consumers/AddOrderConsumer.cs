using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.EventContracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using ProductService.Domain.Entities;
using ProductService.Domain.Enums;
using ProductService.Domain.Interfaces;

namespace ProductService.Infrastructure.Consumers
{
    public class AddOrderConsumer : IConsumer<IAddOrderEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<AddOrderConsumer> _logger;


        public AddOrderConsumer(IProductRepository productRepository, ILogger<AddOrderConsumer> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IAddOrderEvent> context)
        {
            try
            {
                _logger.LogInformation("Processing order event for productId: {productId}", context.Message.productId);

                if (context.Message.productId == null)
                {
                    _logger.LogWarning("Invalid productId in the message.");
                    return;
                }

                var prod = await _productRepository.GetByIdAsync(context.Message.productId);
                if (prod == null)
                {
                    _logger.LogWarning("Product with id {productId} not found", context.Message.productId);
                    return;
                }

                if (prod.Status == ProductStatus.Sold)
                {
                    _logger.LogInformation("Product with id {productId} is already marked as Sold", context.Message.productId);
                    return;
                }

                prod.Status = ProductStatus.Sold;
                await _productRepository.UpdateAsync(prod);

                _logger.LogInformation("Product with id {productId} marked as Sold", context.Message.productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing order event for productId: {productId}", context.Message.productId);
                throw;
            }

        }
    }
}
