using MediatR;

namespace sdg12.Service.Messages
{
    public class AddTagToProductCommand : IRequest<AddTagToProductResponse>
    {
        public AddTagToProductCommand()
        {
        }

        public int ProductId { get; set; }
        public string TagName { get; set; }
        public int UserId { get; set; }
    }

    public class AddTagToProductResponse
    {
    }
}