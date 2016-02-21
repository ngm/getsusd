using MediatR;

namespace sdg12.Service.Messages
{
    public class RemoveTagFromProductCommand : IRequest<RemoveTagFromProductResponse>
    {
        public RemoveTagFromProductCommand()
        {
        }

        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int UserProductTagId { get; set; }
    }

    public class RemoveTagFromProductResponse
    {
    }
}