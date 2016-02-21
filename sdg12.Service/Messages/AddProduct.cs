using MediatR;

namespace sdg12.Service.Messages
{
    public class AddProductCommand : IRequest<AddProductResponse>
    {
        public AddProductCommand()
        {
        }

        public string ProductName { get; set; }
        public string ProductNotes { get; set; }
        public int UserId { get; set; }
    }

    public class AddProductResponse
    {
        public int NewProductId { get; set; }
    }
}