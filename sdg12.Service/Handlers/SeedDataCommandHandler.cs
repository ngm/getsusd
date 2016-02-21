using MediatR;
using NHibernate;
using sdg12.Core.Entities;
using sdg12.Service.Messages;

namespace sdg12.Service.Handlers
{
    public class SeedDataCommandHandler : IRequestHandler<SeedDataCommand, SeedDataResponse>
    {
        private readonly ISession nhSession;

        public SeedDataCommandHandler(ISession nhSession)
        {
            this.nhSession = nhSession;
        }

        public SeedDataResponse Handle(SeedDataCommand command)
        {
            var user = new User
            {
                GivenName = "Bart",
                Surname = "Simpson",
                UserName = "bart"
            };

            using (var tx = nhSession.BeginTransaction())
            {
                nhSession.Save(user);
                tx.Commit();
            }

            return new SeedDataResponse
            {
                UserId = user.Id
            };
        }
    }
}