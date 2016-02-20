using MediatR;

namespace sdg12.Service.Messages
{
    public class EditProductCommand : IRequest<EditProductResponse>
    {
        public EditProductCommand()
        {
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductNotes { get; set; }
        public int UserId { get; set; }
    }

    public class EditProductResponse
    {
    }
}