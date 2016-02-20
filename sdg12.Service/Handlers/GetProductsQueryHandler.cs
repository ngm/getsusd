using MediatR;
using NHibernate;
using sdg12.Core;
using sdg12.Service.Dtos;
using sdg12.Service.Messages;
using System.Collections.Generic;
using System.Linq;

namespace sdg12.Service.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsResponse>
    {
        private readonly ISession nhSession;

        public GetProductsQueryHandler(ISession nhSession)
        {
            this.nhSession = nhSession;
        }

        public GetProductsResponse Handle(GetProductsQuery message)
        {
            var user = nhSession.Get<User>(message.UserId);

            IEnumerable<UserProduct> products;

            if (message.ProductId != null)
            {
                products = user.Products.Where(p => p.Id == message.ProductId);
            }
            else
            {
                products = user.Products;
            }

            return new GetProductsResponse
            {
                Products = products
                    .Select(p => new UserProductDto
                    {
                        ProductId = p.Id,
                        ProductName = p.Name,
                        ProductNotes = p.Notes
                    }).ToList()
            };
        }
    }
}