using MediatR;
using NHibernate;
using NHibernate.Linq;
using sdg12.Core;
using sdg12.Core.Entities;
using sdg12.Service.Messages;
using System.Linq;

namespace sdg12.Service.Handlers
{
    public class AddTagToProductCommandHandler : IRequestHandler<AddTagToProductCommand, AddTagToProductResponse>
    {
        private readonly ISession nhSession;

        public AddTagToProductCommandHandler(ISession nhSession)
        {
            this.nhSession = nhSession;
        }

        public AddTagToProductResponse Handle(AddTagToProductCommand command)
        {
            var user = nhSession.Get<User>(command.UserId);
            var userProduct = nhSession.Get<UserProduct>(command.ProductId);

            var tag = nhSession.Query<Tag>().FirstOrDefault(t => t.Name == command.TagName);
            if (tag == null)
            {
                tag = new Tag { Name = command.TagName };
                using (var tx = nhSession.BeginTransaction())
                {
                    nhSession.Save(tag);
                    tx.Commit();
                }
            }

            var userProductTag = new UserProductTag
            {
                Tag = tag,
                UserProduct = userProduct
            };

            using (var tx = nhSession.BeginTransaction())
            {
                nhSession.Save(userProductTag);
                tx.Commit();
            }

            return new AddTagToProductResponse();
        }
    }
}