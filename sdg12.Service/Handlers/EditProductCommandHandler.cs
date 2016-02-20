using MediatR;
using NHibernate;
using sdg12.Core.Entities;
using sdg12.Service.Messages;
using System.Linq;

namespace sdg12.Service.Handlers
{
    public class EditProductCommandHandler : IRequestHandler<EditProductCommand, EditProductResponse>
    {
        private readonly ISession nhSession;

        public EditProductCommandHandler(ISession nhSession)
        {
            this.nhSession = nhSession;
        }

        public EditProductResponse Handle(EditProductCommand command)
        {
            var user = nhSession.Get<User>(command.UserId);

            var product = user.Products.First(p => p.Id == command.ProductId);
            using (var tx = nhSession.BeginTransaction())
            {
                product.Name = command.ProductName;
                product.Notes = command.ProductNotes;
                nhSession.Update(user);
                tx.Commit();
            }

            return new EditProductResponse();
        }
    }
}