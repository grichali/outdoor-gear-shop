using Contracts.EventContracts;

namespace OrderService.Events
{
    public class AddOrderEvent : IAddOrderEvent
    {
        public string productId { get; set; }
    }
}
