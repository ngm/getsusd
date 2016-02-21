using MediatR;

namespace sdg12.Service.Messages
{
    public class SeedDataCommand : IRequest<SeedDataResponse>
    {
        public SeedDataCommand()
        {
        }
    }

    public class SeedDataResponse
    {
        public int UserId { get; set; }
    }
}