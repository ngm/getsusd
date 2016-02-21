using MediatR;
using NHibernate;
using NHibernate.Linq;
using sdg12.Core;
using sdg12.Core.Entities;
using sdg12.Service.Messages;
using System.Linq;

namespace sdg12.Service.Handlers
{
    public class RemoveTagFromProductCommandHandler : IRequestHandler<RemoveTagFromProductCommand, RemoveTagFromProductResponse>
    {
        private readonly ISession nhSession;

        public RemoveTagFromProductCommandHandler(ISession nhSession)
        {
            this.nhSession = nhSession;
        }

        public RemoveTagFromProductResponse Handle(RemoveTagFromProductCommand command)
        {
            // todo: permissions

            var userProductTag = nhSession.Get<UserProductTag>(command.UserProductTagId);
            if (userProductTag != null)
            {
                using (var tx = nhSession.BeginTransaction())
                {
                    nhSession.Delete(userProductTag);
                    tx.Commit();
                }
            }

            return new RemoveTagFromProductResponse();
        }
    }
}