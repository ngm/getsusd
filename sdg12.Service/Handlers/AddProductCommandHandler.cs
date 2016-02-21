using MediatR;
using NHibernate;
using sdg12.Core.Entities;
using sdg12.Service.Messages;

namespace sdg12.Service.Handlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, AddProductResponse>
    {
        private readonly ISession nhSession;

        public AddProductCommandHandler(ISession nhSession)
        {
            this.nhSession = nhSession;
        }

        public AddProductResponse Handle(AddProductCommand command)
        {
            var user = nhSession.Get<User>(command.UserId);

            var product = new UserProduct
            {
                Name = command.ProductName,
                Notes = command.ProductNotes
            };

            using (var tx = nhSession.BeginTransaction())
            {
                user.Products.Add(product);
                nhSession.Update(user);
                tx.Commit();
            }

            return new AddProductResponse
            {
                NewProductId = product.Id
            };
        }
    }
}