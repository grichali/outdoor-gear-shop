using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.EventContracts;
using MassTransit;

namespace ProductService.Infrastructure.Consumers
{
    public class AddOrderConsumer : IConsumer<IAddOrderEvent>
    {
        public Task Consume(ConsumeContext<IAddOrderEvent> context)
        {
            Console.WriteLine("Message oppo" + context.Message.productId);
            return Task.CompletedTask;
        }
    }
}
